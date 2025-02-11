using Microsoft.Extensions.AI;

namespace CodeAI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IChatClient chatClient = 
            new OllamaChatClient(new Uri("http://localhost:11434/"), "deepseek-r1:1.5b");

        // Start the conversation with context for the AI model
        List<ChatMessage> chatHistory = new();

        while (true)
        {
            // Get user prompt and add to chat history
            Console.WriteLine("Your prompt:");
            var userPrompt = Console.ReadLine();
            chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

            // Stream the AI response and add to chat history
            Console.WriteLine("AI Response:");
            var response = "";
            await foreach (var item in
                           chatClient.CompleteStreamingAsync(chatHistory))
            {
                Console.Write(item.Text);
                response += item.Text;
            }
            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
            Console.WriteLine();
        }
    }
}