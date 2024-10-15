using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;


namespace HelloWorldAI
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            // see youtube: "Genereative AI into ANY .NET App with SemanticKernel"
            //  https://www.youtube.com/watch?v=f_hqGlt_2E8
            //  https://www.youtube.com/watch?v=WxYC9-hBM_g
            // consider
            //  https://github.com/intelligentnode/IntelliServer
            //  https://hub.docker.com/r/intellinode/intelliserver/tags
            //  https://hub.docker.com/r/ndelgado/codellama/tags
            //  https://hub.docker.com/r/dhiltgen/ollama/tags

            HostApplicationBuilder builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings() { 
                Args = args 
            });


#pragma warning disable SKEXP0010	
            builder.Services.AddOpenAIChatCompletion ("llama2", new Uri("http://localhost:11434/"));
#pragma warning restore SKEXP0010	


            IHost app = builder.Build();


            IChatCompletionService chatSvc = app.Services.GetRequiredService<IChatCompletionService>();

            ChatHistory chatHistory = new ChatHistory();


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("AI: What's my context?");
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("User: ");
            string aiContext = Console.ReadLine();
            chatHistory.AddSystemMessage(aiContext);
            
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("AI: How can I help?");
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.Write("User: ");
            string prompt = Console.ReadLine();

            while (true) {

                chatHistory.AddUserMessage(prompt);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("AI: ");
                await foreach (var response in chatSvc.GetStreamingChatMessageContentsAsync(chatHistory)) {
                    Console.Write(response);
                    await Task.Delay(100); // nice UX
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\r\n\r\nUser: ");
                prompt = Console.ReadLine();

            }

        }
    }
}
