// <copyright file="ProblemDetailsContent.cs" company="Corsham Science">
// Copyright (c) Corsham Science. All rights reserved.
// </copyright>

namespace CorshamScience.ProblemDetails.HttpContent
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using CorshamScience.ProblemDetails;

    /// <inheritdoc />
    /// <summary>
    /// A type of <see cref="HttpContent"/> which can contain <see cref="IHttpProblemDetails"/>.
    /// </summary>
    public class ProblemDetailsContent : System.Net.Http.HttpContent
    {
        private readonly MemoryStream _memoryStream;

#pragma warning disable SA1648 // inheritdoc should be used with inheriting class
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDetailsContent" /> class for the provided <see cref="IHttpProblemDetails" />.
        /// </summary>
        /// <param name="problemDetails">The <see cref="IHttpProblemDetails" /> to build the <see cref="ProblemDetailsContent" /> from.</param>
        /// <param name="serializationMethod">The method to write a JSON representation of the provided <see cref="IHttpProblemDetails"/> into a <see cref="Stream"/>.</param>
        public ProblemDetailsContent(IHttpProblemDetails problemDetails, SerializeHttpProblemDetailsJsonToStream serializationMethod)
        {
            ProblemDetails = problemDetails;
            _memoryStream = new MemoryStream();
            serializationMethod(problemDetails, _memoryStream).Wait();
            Headers.ContentType = MediaTypeHeaderValue.Parse("application/problem+json");
        }
#pragma warning restore SA1648 // inheritdoc should be used with inheriting class

        /// <summary>
        /// Gets the <see cref="IHttpProblemDetails"/> this <see cref="HttpContent"/> represents.
        /// </summary>
        public IHttpProblemDetails ProblemDetails { get; }

        /// <inheritdoc />
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context) => await _memoryStream.CopyToAsync(stream);

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            _memoryStream.Dispose();
            base.Dispose(disposing);
        }

        /// <inheritdoc />
        protected override bool TryComputeLength(out long length) => (length = _memoryStream?.Length ?? 0) > 0;
    }
}
