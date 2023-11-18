// AWS credentials and region setup
using Amazon;
using Amazon.Textract;


var accessKey = "AKIAQL4KPJBJ46SRVU3R";
var secretKey = "Nr/tqw92qT6+QXuH/KTHl/mdZ+GFBbNu57DP4jKk";
var region = RegionEndpoint.USWest2; // Change to your desired AWS region

var textractClient = new AmazonTextractClient(accessKey, secretKey, region);

// Load your document as bytes
var documentBytes = File.ReadAllBytes("Horizon Beverage_1.pdf");

// Analyze the document synchronously
var request = new Amazon.Textract.Model.DetectDocumentTextRequest
{
    Document = new Amazon.Textract.Model.Document { 
        
       Bytes =  new MemoryStream(documentBytes)


    }
};

try
{
    var response = await textractClient.DetectDocumentTextAsync(request);

    // Process the results
    if (response.Blocks.Count > 0)
    {
        foreach (var block in response.Blocks)
        {
            if (block.BlockType == "LINE")
            {
                Console.WriteLine(block.Text);
            }
        }
    }
    else
    {
        Console.WriteLine("No text detected in the document.");
    }
}
catch (AmazonTextractException e)
{
    Console.WriteLine("Textract Exception: " + e.Message);
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}