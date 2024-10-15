# HelloWorldAI
This is a first attempt at connecting a console app to a private AI model.


## Objectives
1. The AI model must be contained in a docker image. 
1. The model must be fully offline... everything on one box without network connection!
1. We must be able to "Chat" with it. It's ok if the AI gives garbage back. The AI's performance doesn't matter. 


## The AI Itself
1. Ollama - https://ollama.com/ & https://github.com/ollama/ollama
1. Docker Setup - https://hub.docker.com/r/ollama/ollama
    1. Pull the AI's Container
        1. ```docker run -d -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama```
    1. Pull the specific AI Model(s)
        1. ```docker exec -it ollama ollama run llama3```


## Special Thanks
1. NetworkChuck - https://www.youtube.com/watch?v=WxYC9-hBM_g
1. Nick Chapsas - https://www.youtube.com/watch?v=f_hqGlt_2E8
1. Microsoft's Semantic Kernel
    1. https://learn.microsoft.com/en-us/semantic-kernel/overview/
    1. https://www.nuget.org/packages/Microsoft.SemanticKernel
