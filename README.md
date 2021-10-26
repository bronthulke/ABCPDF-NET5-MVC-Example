# ABCPDF-NET5-MVC-Example
An example of using ABCPDF to generate a PDF in a .NET 5.0 MVC Project.

Uses the ABCPDF library from [WebSupergoo](https://www.websupergoo.com/) to generate a PDF of a partial view.

The partial view gets rendered to a string, and then combined with a template (/wwwroot/resources/pdftemplate.html), which can have CSS styles applied in it.

To run, just clone and run `dotnet run`, and then browse to https://localhost:5001
