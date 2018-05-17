using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using AutoMapper;
using Fusillade;
using Newtonsoft.Json;

using MyPatchSG.DL;
using MyPatchSG.DL.Models;
using MyPatchSG.SAL;
using MyPatchSG.SAL.DTO;
using MyPatchSG.SAL.RequestModels;
using MyPatchSG.BL.Enums;
using MyPatchSG.BL.Managers;
using MyPatchSG.BL.Entities;
using MyPatchSG.BL.Globals;
using MyPatchSG.Utils;

using Xamarin.Forms;

namespace MyPatchSG
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ServiceManager
    {
        public const string APIURL = "http://www.appsbinary.com/MyPatchSGAPI";
        //public const string APIURL = "http://192.168.100.194:8080";
        private readonly IMyPatchSGService _sgService;
        readonly MyPatchSGDatabase _database;
        private readonly IMapper _mapper;
        private string accessToken = "";
        private string tokenType = "";
        private string authToken = "";

        private static volatile ServiceManager sharedInstance;
        private static readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the MyPatchSG.ServiceManager class.
        /// </summary>
        private ServiceManager()
        {
            var sgAPIService = new MyPatchSGAPIService(APIURL);
            var sgService = new MyPatchSGService(sgAPIService);
            _sgService = sgService;

            _database = MyPatchSGDatabase.SharedInstance;

            _mapper = new Bootstrapper().AutoMapper();
        }

        /// <summary>
        /// Gets the shared instance.
        /// </summary>
        /// <value>The shared instance of ServiceManager.</value>
        public static ServiceManager SharedInstance
        {
            get
            {
                if (sharedInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (sharedInstance == null)
                            sharedInstance = new ServiceManager();
                    }
                }

                return sharedInstance;
            }
        }

        public async Task<bool> LoginSession(string username, string password, string clientos)
        {
            var param = new AccessTokenRM(username, password, clientos);
            var response = await _sgService
                .LoginSession(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseDto = new AccessTokenDto();
                //responseDto = await response.Content.ReadAsAsync<AccessTokenDto>(new[] { new JsonMediaTypeFormatter() });
                var responseString = response.Content.ReadAsStringAsync().Result;
                responseDto = JsonConvert.DeserializeObject<AccessTokenDto>(responseString);
                var appinfoDto = JsonConvert.DeserializeObject<AppInfo>(responseDto.appinfo);

                Globals.LoginStatus = LoginStatusType.Success;
                Globals.LoginUsername = responseDto.username;
                Globals.AccessToken = responseDto.access_token;
                Globals.RefreshToken = responseDto.refresh_token;
                Globals.AppInformation = appinfoDto;
                this.accessToken = responseDto.access_token;
                this.tokenType = responseDto.token_type;
                this.authToken = this.tokenType + " " + this.accessToken;

                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseErrorDto = new AccessTokenErrorDto();
                //responseErrorDto = await response.Content.ReadAsAsync<AccessTokenErrorDto>(new[] { new JsonMediaTypeFormatter() });
                //var responseString = response.Content.ReadAsStringAsync().Result;
                //responseErrorDto = JsonConvert.DeserializeObject<AccessTokenErrorDto>(responseString);

                var responseString = "";
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    responseString = new StreamReader(responseStream).ReadToEnd();
                }
                responseErrorDto = JsonConvert.DeserializeObject<AccessTokenErrorDto>(responseString);

                Globals.LoginStatus = LoginStatusType.LoginError;
                Globals.ErrorTitle = responseErrorDto.error;
                Globals.ErrorDescription = responseErrorDto.error_description;

                return true;
            }

            return false;
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            var param = new UserRegistrationRM(username, password);
            var responseDto = await _sgService
                .RegisterUser(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (responseDto != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Login the user.
        /// </summary>
        public async Task<bool> VerifyLogin(string UserID, string Password, string MacAddr)
        {
            var param = new VerifyLoginRM(UserID, Password, MacAddr);
            var response = await _sgService
                .VerifyLogin(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseDto = new LoginResponseDTO();
                var responseString = response.Content.ReadAsStringAsync().Result;
                responseDto = JsonConvert.DeserializeObject<LoginResponseDTO>(responseString);

                Globals.LoginStatus = LoginStatusType.Success;
                Globals.LoginUsername = responseDto.USER_ID;
                Globals.UserType = responseDto.USER_TYPE;
                Globals.UserMessage = responseDto.User_Msg;

                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Globals.LoginStatus = LoginStatusType.LoginError;
                Globals.ErrorTitle = "Error";
                Globals.ErrorDescription = "Not Found";

                return true;
            }

            return false;
        }

        public async Task<bool> GetFileName(string UserName, string AppID, string OS)
        {
            var param = new GetFileNameRM(UserName, AppID, OS);
            var responseDto = await _sgService
                .GetFileName(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (responseDto != null)
            {
                if (responseDto.dbFileNames != null && responseDto.dbFileNames.Count() != 0)
                {
                    foreach (var fileNameInfo in responseDto.dbFileNames)
                    {
                        if (fileNameInfo.file_Type == "Audit_DB")
                        {
                            var auditFileInfo = new DBFileInfo();
                            auditFileInfo.ConvertFromFileInfoDto(fileNameInfo);
                            Globals.AuditDBInfo = auditFileInfo;
                        }
                        else if (fileNameInfo.file_Type == "Master_DB")
                        {
                            var masterFileInfo = new DBFileInfo();
                            masterFileInfo.ConvertFromFileInfoDto(fileNameInfo);
                            Globals.MasterDBInfo = masterFileInfo;
                        }
                    }

                    Globals.GetFileNameResult = GetFileNameResultType.Success;
                    return true;
                }
                else
                {
                    Globals.GetFileNameResult = GetFileNameResultType.Error;
                    return false;
                }
            }

            Globals.GetFileNameResult = GetFileNameResultType.Error;
            return false;
        }

        public async Task<bool> DownloadFile()
        {
            string masterDBURL = Globals.MasterDBInfo.PathHTTP + Globals.MasterDBInfo.FileName;
            string auditDBURL = Globals.AuditDBInfo.PathHTTP + Globals.AuditDBInfo.FileName;

            // For testing
            //masterDBURL = "http://192.168.100.194:8080/DB/Master_DB.zip";
            //auditDBURL = "http://192.168.100.194:8080/DB/Audit_DB.zip";

            var fileUtil = DependencyService.Get<IFileUtil>();

            var customAPIService = new CustomAPIService();
            var response = await customAPIService.DownloadFile(masterDBURL).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = fileUtil.GetTempZipFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        streamToWriteTo.Dispose();
                    }

                    try
                    {
                        using (ZipArchive archive = ZipFile.Open(fileToWriteTo, ZipArchiveMode.Read))
                        {
                            string extractTo = fileUtil.GetTempDirectoryPath();
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                Console.WriteLine("Size: " + entry.CompressedLength);
                                Console.WriteLine("Name: " + entry.Name);

                                string path = Path.Combine(extractTo, "Master_DB.db");
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }

                                entry.ExtractToFile(path);
                                string tempDB = fileUtil.GetTempDBFileName();
                                entry.ExtractToFile(tempDB);
                            }

                            archive.Dispose();
                        }

                        File.Delete(fileToWriteTo);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }
                    streamToReadFrom.Dispose();
                }

                Globals.DownloadMasterDBResult = DownloadDBResultType.Success;


                var responseAudit = await customAPIService.DownloadFile(auditDBURL).ConfigureAwait(false);

                if (responseAudit.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    using (Stream streamToReadFromAudit = await responseAudit.Content.ReadAsStreamAsync())
                    {
                        string fileToWriteToAudit = fileUtil.GetTempZipFileName();
                        using (Stream streamToWriteToAudit = File.Open(fileToWriteToAudit, FileMode.Create))
                        {
                            await streamToReadFromAudit.CopyToAsync(streamToWriteToAudit);
                        }

                        try
                        {
                            using (ZipArchive archiveAudit = ZipFile.Open(fileToWriteToAudit, ZipArchiveMode.Read))
                            {
                                string extractToAudit = fileUtil.GetTempDirectoryPath();
                                foreach (ZipArchiveEntry entry in archiveAudit.Entries)
                                {
                                    Console.WriteLine("Size: " + entry.CompressedLength);
                                    Console.WriteLine("Name: " + entry.Name);

                                    string pathAuditBlank = Path.Combine(extractToAudit, "AuditDB_Blank.db");
                                    if (File.Exists(pathAuditBlank))
                                    {
                                        File.Delete(pathAuditBlank);
                                    }

                                    entry.ExtractToFile(pathAuditBlank);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception: " + ex.Message);
                        }
                    }

                    Globals.DownloadAuditDBResult = DownloadDBResultType.Success;
                }
                else if (responseAudit.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    Globals.DownloadAuditDBResult = DownloadDBResultType.BadRequestError;
                    Globals.ErrorTitle = "Invalid parameters";
                    Globals.ErrorDescription = "Invalid parameters";

                    return true;
                }
                else if (responseAudit.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Globals.DownloadAuditDBResult = DownloadDBResultType.NotFoundError;
                    Globals.ErrorTitle = "File Not Found";
                    Globals.ErrorDescription = "Audit DB File Not Found";

                    return true;
                }

                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Globals.DownloadMasterDBResult = DownloadDBResultType.BadRequestError;
                Globals.ErrorTitle = "Invalid parameters";
                Globals.ErrorDescription = "Invalid parameters";

                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Globals.DownloadMasterDBResult = DownloadDBResultType.NotFoundError;
                Globals.ErrorTitle = "File Not Found";
                Globals.ErrorDescription = "Master DB File Not Found";

                return true;
            }

            return false;
        }

        public async  Task<bool> DownloadAuditDBFile()
        {
            string auditDBURL = Globals.AuditDBInfo.PathHTTP + Globals.AuditDBInfo.FileName;

            //auditDBURL = "http://192.168.100.194:8080/DB/Audit_DB.zip";

            var fileUtil = DependencyService.Get<IFileUtil>();

            var customAPIService = new CustomAPIService();
            
            var responseAudit = await customAPIService.DownloadFile(auditDBURL).ConfigureAwait(false);

            if (responseAudit.StatusCode == System.Net.HttpStatusCode.OK)
            {
                using (Stream streamToReadFromAudit = await responseAudit.Content.ReadAsStreamAsync())
                {
                    string fileToWriteToAudit = fileUtil.GetTempZipFileName();
                    using (Stream streamToWriteToAudit = File.Open(fileToWriteToAudit, FileMode.Create))
                    {
                        await streamToReadFromAudit.CopyToAsync(streamToWriteToAudit);
                    }

                    try
                    {
                        using (ZipArchive archiveAudit = ZipFile.Open(fileToWriteToAudit, ZipArchiveMode.Read))
                        {
                            string extractToAudit = fileUtil.GetTempDirectoryPath();
                            foreach (ZipArchiveEntry entry in archiveAudit.Entries)
                            {
                                Console.WriteLine("Size: " + entry.CompressedLength);
                                Console.WriteLine("Name: " + entry.Name);

                                string pathAudit = Path.Combine(extractToAudit, "Audit_DB.db");
                                if (File.Exists(pathAudit))
                                {
                                    File.Delete(pathAudit);
                                }

                                entry.ExtractToFile(pathAudit);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception: " + ex.Message);
                    }
                }

                Globals.DownloadAuditDBResult = DownloadDBResultType.Success;
            }
            else if (responseAudit.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Globals.DownloadAuditDBResult = DownloadDBResultType.BadRequestError;
                Globals.ErrorTitle = "Invalid parameters";
                Globals.ErrorDescription = "Invalid parameters";

                return true;
            }
            else if (responseAudit.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Globals.DownloadAuditDBResult = DownloadDBResultType.NotFoundError;
                Globals.ErrorTitle = "File Not Found";
                Globals.ErrorDescription = "Audit DB File Not Found";

                return true;
            }

            return false;
        }

        public async Task<bool> UploadAuditDBFile(string Username)
        {
            var fileUtil = DependencyService.Get<IFileUtil>();
            string auditDBPath = fileUtil.GetAuditDBPath();
            string tempDirectoryPath = fileUtil.GetTempDirectoryPath();
            string auditZipPath = Path.Combine(tempDirectoryPath, Username + ".zip");

            if (File.Exists(auditZipPath))
            {
                File.Delete(auditZipPath);
            }

            try
            {
                using (ZipArchive zip = ZipFile.Open(auditZipPath, ZipArchiveMode.Create))
                {
                    zip.CreateEntryFromFile(auditDBPath, "AuditDB.db");
                }

                if (File.Exists(auditZipPath))
                {
                    var response = await _sgService
                            .UploadResource(Priority.UserInitiated, Username, Username + ".zip", File.ReadAllBytes(auditZipPath))
                            .ConfigureAwait(false);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseDto = new ResourceUploadSuccessDto();
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        responseDto = JsonConvert.DeserializeObject<ResourceUploadSuccessDto>(responseString);

                        Globals.UploadAuditDBResult = UploadDBResultType.Success;
                        Globals.UploadDate = responseDto.UploadDate;
                        Globals.FilePath = responseDto.FilePath;
                        Globals.ErrorTitle = "";
                        Globals.ErrorDescription = "";

                        return true;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var responseErrorDto = new ResourceUploadFailureDto();
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        responseErrorDto = JsonConvert.DeserializeObject<ResourceUploadFailureDto>(responseString);

                        Globals.UploadAuditDBResult = UploadDBResultType.BadRequestError;
                        Globals.ErrorTitle = responseErrorDto.Error;
                        Globals.ErrorDescription = responseErrorDto.Error_Description;
                        Globals.UploadDate = responseErrorDto.UploadDate;

                        return true;
                    }

                    return false;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return false;
        }

        public async Task<bool> AddDownloadLog(string DownloadDate, string LoginID, string Status, string DBPath, string TotalTime, string AppVersion, string OSType, string OSVersion, string MacAddr)
        {
            var param = new AddDownloadLogRM(DownloadDate, LoginID, Status, DBPath, TotalTime, AppVersion, OSType, OSVersion, MacAddr);
            var response = await _sgService
                .AddDownloadLog(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> AddUploadLog(string UploadDate, string LoginID, string Status, string FilePath, string TotalTime, string AppVersion, string OSType, string OSVersion, string MacAddr)
        {
            var param = new AddUploadLogRM(UploadDate, LoginID, Status, FilePath, TotalTime, AppVersion, OSType, OSVersion, MacAddr);
            var response = await _sgService
                .AddUploadLog(Priority.UserInitiated, param)
                .ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}

