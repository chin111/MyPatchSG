using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Android;
using Android.Preferences;

namespace MyPatchSG.Droid.Utils
{
    public class AppPreferences
    {
        private ISharedPreferences mSharedPrefs;
        private ISharedPreferencesEditor mPrefsEditor;
        private Context mContext;

        private static String PREFERENCE_USERNAME_KEY = "USERNAME_KEY";
        private static String PREFERENCE_PASSWORD_KEY = "PASSWORD_KEY";
        private static String PREFERENCE_MASTERDB_DOWNLOADED_KEY = "MASTERDB_DOWNLOADED_KEY";
        private static String PREFERENCE_AUDITDB_DOWNLOADED_KEY = "AUDITDB_DOWNLOADED_KEY";
        private static String PREFERENCE_LOGIN_SKIPPED_KEY = "LOGIN_SKIPPED_KEY";

        private static String PREFERENCE_MASTERDBINFO_FILENAME_KEY = "MASTERDBINFO_FILENAME_KEY";
        private static String PREFERENCE_MASTERDBINFO_FILESIZE_KEY = "MASTERDBINFO_FILESIZE_KEY";
        private static String PREFERENCE_MASTERDBINFO_FILETYPE_KEY = "MASTERDBINFO_FILETYPE_KEY";
        private static String PREFERENCE_MASTERDBINFO_ISCOMPLETE_KEY = "MASTERDBINFO_ISCOMPLETE_KEY";
        private static String PREFERENCE_MASTERDBINFO_PATHHTTP_KEY = "MASTERDBINFO_PATHHTTP_KEY";
        private static String PREFERENCE_MASTERDBINFO_VERSION_KEY = "MASTERDBINFO_VERSION_KEY";

        public AppPreferences(Context context)
        {
            this.mContext = context;
            mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            mPrefsEditor = mSharedPrefs.Edit();
        }
        public void SaveUsername(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_USERNAME_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetUsername()
        {
            return mSharedPrefs.GetString(PREFERENCE_USERNAME_KEY, "");
        }
        public void SavePassword(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_PASSWORD_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetPassword()
        {
            return mSharedPrefs.GetString(PREFERENCE_PASSWORD_KEY, "");
        }
        public void SaveMasterDBDownloaded(bool value)
        {
            mPrefsEditor.PutBoolean(PREFERENCE_MASTERDB_DOWNLOADED_KEY, value);
            mPrefsEditor.Commit();
        }
        public bool GetMasterDBDownloaded()
        {
            return mSharedPrefs.GetBoolean(PREFERENCE_MASTERDB_DOWNLOADED_KEY, false);
        }
        public void SaveAuditDBDownloaded(bool value)
        {
            mPrefsEditor.PutBoolean(PREFERENCE_AUDITDB_DOWNLOADED_KEY, value);
            mPrefsEditor.Commit();
        }
        public bool GetAuditDBDownloaded()
        {
            return mSharedPrefs.GetBoolean(PREFERENCE_AUDITDB_DOWNLOADED_KEY, false);
        }
        public void SaveLoginSkipped(bool value)
        {
            mPrefsEditor.PutBoolean(PREFERENCE_LOGIN_SKIPPED_KEY, value);
            mPrefsEditor.Commit();
        }
        public bool GetLoginSkipped()
        {
            return mSharedPrefs.GetBoolean(PREFERENCE_LOGIN_SKIPPED_KEY, false);
        }
        public void SaveMasterDBInfoFileName(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_MASTERDBINFO_FILENAME_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetMasterDBInfoFileName()
        {
            return mSharedPrefs.GetString(PREFERENCE_MASTERDBINFO_FILENAME_KEY, "");
        }
        public void SaveMasterDBInfoFileType(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_MASTERDBINFO_FILETYPE_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetMasterDBInfoFileType()
        {
            return mSharedPrefs.GetString(PREFERENCE_MASTERDBINFO_FILETYPE_KEY, "");
        }
        public void SaveMasterDBInfoFileSize(long value)
        {
            mPrefsEditor.PutLong(PREFERENCE_MASTERDBINFO_FILESIZE_KEY, value);
            mPrefsEditor.Commit();
        }
        public long GetMasterDBInfoFileSize()
        {
            return mSharedPrefs.GetLong(PREFERENCE_MASTERDBINFO_FILESIZE_KEY, 0);
        }
        public void SaveMasterDBInfoIsComplete(int value)
        {
            mPrefsEditor.PutInt(PREFERENCE_MASTERDBINFO_ISCOMPLETE_KEY, value);
            mPrefsEditor.Commit();
        }
        public int GetMasterDBInfoIsComplete()
        {
            return mSharedPrefs.GetInt(PREFERENCE_MASTERDBINFO_ISCOMPLETE_KEY, 0);
        }
        public void SaveMasterDBInfoPathHttp(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_MASTERDBINFO_PATHHTTP_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetMasterDBInfoPathHttp()
        {
            return mSharedPrefs.GetString(PREFERENCE_MASTERDBINFO_PATHHTTP_KEY, "");
        }
        public void SaveMasterDBInfoVersion(string value)
        {
            mPrefsEditor.PutString(PREFERENCE_MASTERDBINFO_VERSION_KEY, value);
            mPrefsEditor.Commit();
        }
        public string GetMasterDBInfoVersion()
        {
            return mSharedPrefs.GetString(PREFERENCE_MASTERDBINFO_VERSION_KEY, "");
        }
    }
}