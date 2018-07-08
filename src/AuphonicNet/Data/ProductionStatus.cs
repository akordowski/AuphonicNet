namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents the production status enumeration.
    /// </summary>
    public enum ProductionStatus
    {
        /// <summary>
        /// File Upload
        /// </summary>
        FileUpload = 0,

        /// <summary>
        /// Waiting
        /// </summary>
        Waiting = 1,

        /// <summary>
        /// Error
        /// </summary>
        Error = 2,

        /// <summary>
        /// Done
        /// </summary>
        Done = 3,

        /// <summary>
        /// Audio Processing
        /// </summary>
        AudioProcessing = 4,

        /// <summary>
        /// Audio Encoding
        /// </summary>
        AudioEncoding = 5,

        /// <summary>
        /// Outgoing File Transfer
        /// </summary>
        OutgoingFileTransfer = 6,

        /// <summary>
        /// Audio Mono Mixdown
        /// </summary>
        AudioMonoMixdown = 7,

        /// <summary>
        /// Split Audio On Chapter Marks
        /// </summary>
        SplitAudioOnChapterMarks = 8,

        /// <summary>
        /// Incomplete Form
        /// </summary>
        IncompleteForm = 9,

        /// <summary>
        /// Production Not Started Yet
        /// </summary>
        ProductionNotStartedYet = 10,

        /// <summary>
        /// Production Outdated
        /// </summary>
        ProductionOutdated = 11,

        /// <summary>
        /// Incoming File Transfer
        /// </summary>
        IncomingFileTransfer = 12,

        /// <summary>
        /// Stopping the Production
        /// </summary>
        StoppingProduction = 13,

        /// <summary>
        /// Speech Recognition
        /// </summary>
        SpeechRecognition = 14
    }
}