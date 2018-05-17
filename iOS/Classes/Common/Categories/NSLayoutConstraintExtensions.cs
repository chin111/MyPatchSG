using System;

using Foundation;
using UIKit;

namespace MyPatchSG.iOS
{
	public static class NSLayoutConstraintExtension
	{
		public static NSLayoutConstraint UpdateMultiplier(this NSLayoutConstraint constraint, nfloat multiplier)
		{
			NSObject firstItem = constraint.FirstItem;
			NSObject secondItem = constraint.SecondItem;

			NSLayoutAttribute firstAttribute = constraint.FirstAttribute;
			NSLayoutAttribute secondAttribute = constraint.SecondAttribute;

			NSLayoutConstraint newConstraint = NSLayoutConstraint.Create(firstItem, firstAttribute, constraint.Relation, secondItem, secondAttribute, multiplier, constraint.Constant);
			newConstraint.Priority = constraint.Priority;
			newConstraint.ShouldBeArchived = constraint.ShouldBeArchived;
			newConstraint.SetIdentifier(constraint.GetIdentifier());
			newConstraint.Active = true;

			NSLayoutConstraint.DeactivateConstraints(new NSLayoutConstraint[] { constraint });
			NSLayoutConstraint.DeactivateConstraints(new NSLayoutConstraint[] { newConstraint });

			return newConstraint;
		}
	}
}
