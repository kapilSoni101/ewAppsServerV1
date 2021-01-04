using System;

namespace ewApps.Core.Services.DI {
    [Flags]
    public enum CoreServiceEnum {

        AppDeeplinkService = 1,
        DeeplinkServices = 2,
        EmailService = 4,
        ExceptionService = 8,
        ExportService = 16,
        GeocodingService = 32,
        LoggingService = 64,
        Money = 128,
        NotificationService = 256,
        ScheduledJobService = 512,
        SerilogLoggingService = 1024,
        SMSService = 2048,
        StorageService = 4096,
        DMService = 8192,
        UserSessionService = 16384,
        WebhookServer = 32768,
        WebhookSubscription = 65536,
        ConnectionManager = 131072,
        ServiceProcessor = 262144,
        UniqueIdentityGenerator= 524288
    }

}
