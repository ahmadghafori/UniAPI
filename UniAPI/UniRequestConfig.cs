namespace UniAPI
{
    public class UniRequestConfig
    {
        /// <summary>
        /// حداکثر زمان انتظار برای پاسخ
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        /// <summary>
        /// تعداد دفعات Retry در صورت Failure
        /// </summary>
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// فاصله بین Retry ها
        /// </summary>
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);

        /// <summary>
        /// اگر true باشد، جزئیات درخواست و پاسخ در log چاپ می‌شود
        /// </summary>
        public bool EnableLogging { get; set; } = false;

        /// <summary>
        /// Callback برای دریافت داده‌های خطا یا لاگ
        /// - پارامتر اول: UniRequest
        /// - پارامتر دوم: StatusCode
        /// - پارامتر سوم: پیام خطا یا response body
        /// </summary>
        public Action<UniRequest<object>, int, string>? OnError { get; set; }
    }

}
