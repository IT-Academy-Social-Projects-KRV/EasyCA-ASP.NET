﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AuthMicroservice.Domain.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AuthMicroservice.Domain.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t send the confirmation link.
        /// </summary>
        public static string ConfirmationLinkNotSent {
            get {
                return ResourceManager.GetString("ConfirmationLinkNotSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The confirmation link has been sent to your email.
        /// </summary>
        public static string ConfirmationLinkSent {
            get {
                return ResourceManager.GetString("ConfirmationLinkSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This email address has already been confirmed.
        /// </summary>
        public static string EmailAlreadyConfirmed {
            get {
                return ResourceManager.GetString("EmailAlreadyConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email has not been confirmed.
        /// </summary>
        public static string EmailNotConfirmed {
            get {
                return ResourceManager.GetString("EmailNotConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email was not send.
        /// </summary>
        public static string EmailNotSend {
            get {
                return ResourceManager.GetString("EmailNotSend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email send successfully.
        /// </summary>
        public static string EmailSend {
            get {
                return ResourceManager.GetString("EmailSend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have successfully logged into your account.
        /// </summary>
        public static string LoginSucceeded {
            get {
                return ResourceManager.GetString("LoginSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid email or password.
        /// </summary>
        public static string LoginWrongCredentials {
            get {
                return ResourceManager.GetString("LoginWrongCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your password has been successfully reset.
        /// </summary>
        public static string PasswordResetSuccess {
            get {
                return ResourceManager.GetString("PasswordResetSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Passwords do not match.
        /// </summary>
        public static string PasswordsNotMatching {
            get {
                return ResourceManager.GetString("PasswordsNotMatching", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t register a new account.
        /// </summary>
        public static string RegistrationFailed {
            get {
                return ResourceManager.GetString("RegistrationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have successfully registered a new account.
        /// </summary>
        public static string RegistrationSucceeded {
            get {
                return ResourceManager.GetString("RegistrationSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t send a password reset link.
        /// </summary>
        public static string RestoreLinkNotSent {
            get {
                return ResourceManager.GetString("RestoreLinkNotSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A password reset link has been sent to your email.
        /// </summary>
        public static string RestoreLinkSent {
            get {
                return ResourceManager.GetString("RestoreLinkSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have successfully added a new vehicle.
        /// </summary>
        public static string TransportAddingSucceeded {
            get {
                return ResourceManager.GetString("TransportAddingSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid vehicle category.
        /// </summary>
        public static string TransportCategoryNotFound {
            get {
                return ResourceManager.GetString("TransportCategoryNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Coudn&apos;t delete the vehicle.
        /// </summary>
        public static string TransportDeleteFailed {
            get {
                return ResourceManager.GetString("TransportDeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The vehicle has been successfully deleted.
        /// </summary>
        public static string TransportDeleteSucceeded {
            get {
                return ResourceManager.GetString("TransportDeleteSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The vehicle has not been found.
        /// </summary>
        public static string TransportNotFound {
            get {
                return ResourceManager.GetString("TransportNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There are no vehicles.
        /// </summary>
        public static string TransportsNotFound {
            get {
                return ResourceManager.GetString("TransportsNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have successfully updated your vehicle information.
        /// </summary>
        public static string TransportUpdatingSucceeded {
            get {
                return ResourceManager.GetString("TransportUpdatingSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t find such user.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user does not have any personal data.
        /// </summary>
        public static string UserPersonalDataNotFound {
            get {
                return ResourceManager.GetString("UserPersonalDataNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refresh token is not active.
        /// </summary>
        public static string UserRefreshTokenNotActive {
            get {
                return ResourceManager.GetString("UserRefreshTokenNotActive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User has another Refresh token.
        /// </summary>
        public static string UserRefreshTokenNotFound {
            get {
                return ResourceManager.GetString("UserRefreshTokenNotFound", resourceCulture);
            }
        }
    }
}
