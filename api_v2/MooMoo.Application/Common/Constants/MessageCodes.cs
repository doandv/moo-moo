namespace MooMoo.Application.Common.Constants;

/// <summary>
/// Translation keys for frontend i18n mapping
/// Backend returns codes, frontend maps to localized messages
/// </summary>
public static class MessageCodes
{
    // ==================== Errors ====================
    // Authentication & Registration
    public const string ERROR_EMAIL_ALREADY_EXISTS = "ERROR_EMAIL_ALREADY_EXISTS";
    public const string ERROR_INVALID_CREDENTIALS = "ERROR_INVALID_CREDENTIALS";
    public const string ERROR_ACCOUNT_LOCKED = "ERROR_ACCOUNT_LOCKED";
    public const string ERROR_ACCOUNT_NOT_VERIFIED = "ERROR_ACCOUNT_NOT_VERIFIED";
    
    // Validation
    public const string ERROR_VALIDATION_ERROR = "ERROR_VALIDATION_ERROR";
    public const string ERROR_INVALID_EMAIL_FORMAT = "ERROR_INVALID_EMAIL_FORMAT";
    public const string ERROR_PASSWORD_TOO_WEAK = "ERROR_PASSWORD_TOO_WEAK";
    public const string ERROR_REQUIRED_FIELD_MISSING = "ERROR_REQUIRED_FIELD_MISSING";
    
    // Authorization
    public const string ERROR_UNAUTHORIZED = "ERROR_UNAUTHORIZED";
    public const string ERROR_FORBIDDEN = "ERROR_FORBIDDEN";
    public const string ERROR_TOKEN_EXPIRED = "ERROR_TOKEN_EXPIRED";
    public const string ERROR_INVALID_TOKEN = "ERROR_INVALID_TOKEN";
    
    // Resources
    public const string ERROR_RESOURCE_NOT_FOUND = "ERROR_RESOURCE_NOT_FOUND";
    public const string ERROR_USER_NOT_FOUND = "ERROR_USER_NOT_FOUND";
    public const string ERROR_PROFILE_NOT_FOUND = "ERROR_PROFILE_NOT_FOUND";
    public const string ERROR_MISSION_NOT_FOUND = "ERROR_MISSION_NOT_FOUND";
    
    // Business Logic
    public const string ERROR_INSUFFICIENT_BALANCE = "ERROR_INSUFFICIENT_BALANCE";
    public const string ERROR_MISSION_ALREADY_COMPLETED = "ERROR_MISSION_ALREADY_COMPLETED";
    public const string ERROR_INVALID_OPERATION = "ERROR_INVALID_OPERATION";
    
    // Server Errors
    public const string ERROR_INTERNAL_SERVER_ERROR = "ERROR_INTERNAL_SERVER_ERROR";
    public const string ERROR_SERVICE_UNAVAILABLE = "ERROR_SERVICE_UNAVAILABLE";

    // ==================== Success Messages ====================
    public const string SUCCESS_ACCOUNT_CREATED = "SUCCESS_ACCOUNT_CREATED";
    public const string SUCCESS_LOGIN_SUCCESS = "SUCCESS_LOGIN_SUCCESS";
    public const string SUCCESS_LOGOUT_SUCCESS = "SUCCESS_LOGOUT_SUCCESS";
    public const string SUCCESS_PASSWORD_RESET_SENT = "SUCCESS_PASSWORD_RESET_SENT";
    public const string SUCCESS_PASSWORD_CHANGED = "SUCCESS_PASSWORD_CHANGED";
    public const string SUCCESS_PROFILE_UPDATED = "SUCCESS_PROFILE_UPDATED";
    public const string SUCCESS_AVATAR_UPLOADED = "SUCCESS_AVATAR_UPLOADED";
    public const string SUCCESS_MISSION_COMPLETED = "SUCCESS_MISSION_COMPLETED";
    public const string SUCCESS_REWARD_CLAIMED = "SUCCESS_REWARD_CLAIMED";
    public const string SUCCESS_POINTS_EARNED = "SUCCESS_POINTS_EARNED";

    // ==================== Labels & Field Names ====================
    public const string LABEL_EMAIL = "LABEL_EMAIL";
    public const string LABEL_PASSWORD = "LABEL_PASSWORD";
    public const string LABEL_NAME = "LABEL_NAME";
    public const string LABEL_CONFIRM_PASSWORD = "LABEL_CONFIRM_PASSWORD";
    public const string LABEL_LOGIN = "LABEL_LOGIN";
    public const string LABEL_REGISTER = "LABEL_REGISTER";
    public const string LABEL_SUBMIT = "LABEL_SUBMIT";
    public const string LABEL_CANCEL = "LABEL_CANCEL";
    public const string LABEL_SAVE = "LABEL_SAVE";
    public const string LABEL_DELETE = "LABEL_DELETE";
    public const string LABEL_MISSION = "LABEL_MISSION";
    public const string LABEL_REWARD = "LABEL_REWARD";
    public const string LABEL_PROFILE = "LABEL_PROFILE";
    public const string LABEL_PARENT = "LABEL_PARENT";
    public const string LABEL_CHILD = "LABEL_CHILD";

    // ==================== Notifications ====================
    public const string NOTIFICATION_NEW_MISSION_AVAILABLE = "NOTIFICATION_NEW_MISSION_AVAILABLE";
    public const string NOTIFICATION_REWARD_EXPIRING_SOON = "NOTIFICATION_REWARD_EXPIRING_SOON";
    public const string NOTIFICATION_CHILD_COMPLETED_MISSION = "NOTIFICATION_CHILD_COMPLETED_MISSION";
    public const string NOTIFICATION_POINTS_RECEIVED = "NOTIFICATION_POINTS_RECEIVED";

    // ==================== Validation Messages ====================
    public const string VALIDATION_EMAIL_REQUIRED = "VALIDATION_EMAIL_REQUIRED";
    public const string VALIDATION_EMAIL_INVALID = "VALIDATION_EMAIL_INVALID";
    public const string VALIDATION_PASSWORD_REQUIRED = "VALIDATION_PASSWORD_REQUIRED";
    public const string VALIDATION_PASSWORD_MIN_LENGTH = "VALIDATION_PASSWORD_MIN_LENGTH";
    public const string VALIDATION_PASSWORD_MISMATCH = "VALIDATION_PASSWORD_MISMATCH";
    public const string VALIDATION_NAME_REQUIRED = "VALIDATION_NAME_REQUIRED";
    public const string VALIDATION_NAME_MAX_LENGTH = "VALIDATION_NAME_MAX_LENGTH";
}
