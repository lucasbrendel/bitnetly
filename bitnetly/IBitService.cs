using System;
using System.Collections.Generic;

namespace bitnetly
{
    /// <summary>
    /// Defines the possible return codes when calling the various methods in the library
    /// </summary>
    public enum StatusCode
    {
        OK = 0,
        Unspecified = 1,
        UnknownError = 101,
        MissingParameter = 201,
        UndefinedMethod = 202,
        NotAuthenticated = 203,
        AlreadyLoggedIn = 204,
        InvalidCredentials = 205,
        InvalidVersion = 206,
        PostError = 207,
        RateLimitExceeded = 208,
        PageNotFound = 404,
        ServiceUnavailable = 503,
        InvalidEmail = 1101,
        InvalidUsername = 1102,
        UsernameNotAvailable = 1103,
        EmailNotAvailable = 1104,
        InvalidPasswordLength = 1105,
        CouldNotFetchJSON = 1201,
        InfoNotFoundForDocument = 1202,
        InfoNotFoundForDocument2 = 1203,
        InvalidBitlyHash = 1204,
        TrafficLookupHashFailed = 1205,
        InvalidURLOrAlreadyShort = 1206,
        CNAMEAlreadyAssociated = 1207,
        CNAMEIsInvalid = 1208,
        NoAccessToCNAMEVersion = 1209,
        CNAMENotAuthenticated = 1210,
        ErrorInBatch = 1211,
        KeywordInUse = 1212,
        NoMatchingLongURL = 1213,
        InvalidTwitterCredentials = 1301,
        MissingTwitterCredentials = 1302,
        ErrorUpdatingTwitterStatus = 1303,
        TextToLong = 1304,
        TwitterPasswordMismatchOnBitly = 1305,
        TwitterUnavailable = 1306,
        DuplicateLinkedAccounts = 1401,
        InvalidAccountType = 1402
    }

    public interface IBitService
    {
        StatusCode Shorten(string url, out string shortened);

        string Shorten(string url);

        IBitResponse[] Shorten(string[] longUrls, out StatusCode statusCode);
    }
}
