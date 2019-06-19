// <copyright file="SerializeHttpProblemDetailsJsonToStream.cs" company="Cognisant Research">
// Copyright (c) Cognisant Research. All rights reserved.
// </copyright>

namespace CR.ProblemDetails.HttpContent
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// A delegate which can be used to change the JSON serialization logic for <see cref="IHttpProblemDetails"/> in a <see cref="ProblemDetailsContent"/>.
    /// </summary>
    /// <param name="details">The <see cref="IHttpProblemDetails"/> to serialize.</param>
    /// <param name="stream">The <see cref="Stream"/> to write the serialized <see cref="IHttpProblemDetails"/> into.</param>
    /// <returns>A task for the serialization &amp; writing of the <see cref="IHttpProblemDetails"/> into the <see cref="Stream"/>.</returns>
    public delegate Task SerializeHttpProblemDetailsJsonToStream(IHttpProblemDetails details, Stream stream);
}
