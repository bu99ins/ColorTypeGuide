using System.Text;
using ColorTypeGuide.AiClientFactory;
using Microsoft.Extensions.AI;

const string systemPrompt =
    """
    **System Role & Task** You are a highly specialized computer vision and colorimetry analysis agent. Your sole task is to analyze an attached photograph of a person and accurately determine their color type.
    
    **Analysis Instructions** Carefully examine the provided image and evaluate the following visual parameters:
    
    1. **Eye Color:** Describe the primary eye color and any distinct patterns or secondary hues.
        
    2. **Hair Tone:** Determine the apparent natural hair tone (e.g., fair, medium, dark).
          
    3. **Hair Color:** Determine the apparent natural hair color and its temperature (warm/cool).
    	
    4. **Skin Tone, Undertone and Luminance:** Identify the surface skin tone (e.g., fair, medium, deep), the underlying hue (cool, warm, or neutral/olive), and the luminance qualities (bright, soft, or neutral).
        
    5. **Contrast Level:** Evaluate the overall value contrast between the person's skin, eyes, and hair (Low, Medium, or High).
        
    
    **Output Constraints**
    
    - Do NOT assume the subject's gender. The analysis applies to any human.
        
    - Do NOT output any conversational text, greetings, explanations, or markdown formatting (such as ```json) outside of the raw JSON block.
        
    - Return the exact result strictly as a valid JSON object following the schema below.
        
    
    **Expected JSON Schema** 
    {
    	"analyzedParameters": {
    		"eyeColor": "String. Specific description of the eye color.",
    		"hair": {
    			"tone": "Enum. The natural hair tone: Fair, Medium, Dark).",
    			"color": "String. Specific description of the hair color and its temperature."
    		},
    		"skin": {
    			"tone": "Enum. The surface skin tone: Fair, Medium, Deep).",
    			"undertone": "Enum. Cool, Warm, Neutral.",
    			"luminance": "Enum. Bright, Soft, Neutral."
    		},
    		"contrastLevel": "String. Low, Medium, or High."
    	},
    	
    	"colorType": "Enum. The final determined seasonal color type of 16: LightSpring, WarmSpring, BrightSpring, SoftSpring, LightSummer, CoolSummer, SoftSummer, DeepSummer, SoftAutumn, WarmAutumn, DeepAutumn, LightAutumn, CoolWinter, BrightWinter, DeepWinter, SoftWinter."
    }
    """;

var systemMessage = new ChatMessage(ChatRole.System, systemPrompt);

var noReasoningOptions = new ChatOptions
{
    Temperature = 0,
    //ResponseFormat = ChatResponseFormat.Json
};
//var ollamaOptions = new ChatOptions
//{
//    Temperature = 0,
//    ResponseFormat = ChatResponseFormat.Json,
//    AdditionalProperties = new AdditionalPropertiesDictionary
//    {
//        ["num_thread"] = 8,
//        ["num_gpu"] = 18,
//        //["num_ctx"] = 4096
//    }
//};

var clientFactory = new OpenAiClientFactory("gpt-4o");
var completion = clientFactory.CreateCompletion(noReasoningOptions);
//var clientFactory = new OllamaClientFactory("deepseek-r1:7b");
//var completion = clientFactory.CreateCompletion(ollamaOptions);

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

using var cts = new CancellationTokenSource();
ConsoleCancelEventHandler handler = (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
    Console.WriteLine("\n\n[System interruption] Releasing CPU/GPU…");
};

Console.CancelKeyPress += handler;

try
{
    var message = new ChatMessage(ChatRole.User, "Define the seasonal color type for the attached image.");
    message.Contents.Add(new DataContent(File.ReadAllBytes(@"C:\_Projects\ColorTypeGuide\data\pic1.jpg"), "image/jpeg"));
        
    // Get response from the model
    Console.ForegroundColor = ConsoleColor.White;
    var (_, lastReason) = await completion.CompleteAsync([systemMessage, message], cts.Token);

    if (lastReason != null && lastReason != ChatFinishReason.Stop)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: The model did not finish properly. Reason: {lastReason}");
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    Console.CancelKeyPress -= handler;
    Console.ResetColor();
    cts.Cancel();
}
