namespace SGOUtil
{

    /// <summary>
    /// Objeto de respuesta para los casos sean satisfactorios o tambien los que no
    /// </summary>
    public class Respuesta
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// Actual response if succeed 
        /// </summary>
        /// <value>
        /// Actual response if succeed 
        /// </value>
        public object Data { get; set; } = null;

        /// <summary>
        /// Remark if anythig to convey
        /// </summary>
        /// <value>
        /// Remark if anythig to convey
        /// </value>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public object Error { get; set; } = null;
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string StatusCode { get; set; } = null;
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// En caso se necesite manejar algun codigo de respuesta
        /// </summary>
        public string Codigo { get; set; }
    }


}

