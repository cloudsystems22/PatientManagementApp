# PatientManagementApp

O **PatientManagementApp** é um sistema para gerenciamento de pacientes, desenvolvido com .NET MAUI Blazor Hybrid. Ele permite a gestão de informações de pacientes, triagens e atendimentos em uma aplicação multi-plataforma (Windows, Android, iOS, macOS).

## ✨ Funcionalidades

-   Cadastro e gerenciamento de pacientes.
-   Registro de triagens.
-   Histórico de atendimentos.
-   Interface de usuário moderna e responsiva.

## 🚀 Tecnologias Utilizadas

-   **.NET MAUI**: Framework para criação de aplicações nativas multi-plataforma com C# e XAML.
-   **Blazor Hybrid**: Permite hospedar componentes Razor em uma aplicação nativa .NET MAUI.
-   **C#**: Linguagem de programação principal.
-   **HTML/CSS**: Para a construção da interface do usuário com componentes Blazor.

## 📊 Arquitetura e Fluxo de Dados

A aplicação utiliza o padrão .NET MAUI Blazor Hybrid. A interface do usuário é construída com componentes Blazor que são renderizados em uma `BlazorWebView` dentro da aplicação nativa. Os serviços (como `PacienteService.cs`) são responsáveis por se comunicar com uma API backend (não inclusa neste repositório) para buscar e enviar dados.

```mermaid
graph TD
    subgraph "PatientManagement.App (Cliente Nativo)"
        A[Interface do Usuário <br> (Páginas Blazor)]
        B[Serviços <br> (PacienteService.cs)]
        C[Componentes Nativos <br> (.NET MAUI)]
    end

    subgraph "Backend"
        D[API REST]
        E[Banco de Dados]
    end

    A -- Interage com --> B
    B -- Requisições HTTP --> D
    D -- Acessa --> E
    D -- Respostas JSON --> B
    B -- Atualiza --> A
    C -- Hospeda --> A
```

## 📂 Estrutura do Projeto

```
PatientManagement.App/
│
├── Platforms/              → Código específico para cada plataforma (Android, iOS, etc.)
├── wwwroot/                → Arquivos estáticos (CSS, JS, imagens)
├── Pages/                  → Páginas Blazor (Telas da aplicação)
│   ├── Pacientes.razor
│   └── Triagem.razor
│
├── Components/             → Componentes Blazor reutilizáveis
│   └── PacienteForm.razor
│
├── Services/               → Serviços para chamadas HTTP à API
│   └── PacienteService.cs
│
├── MauiProgram.cs          → Ponto de entrada e configuração da aplicação .NET MAUI.
└── MainLayout.razor        → Layout principal das páginas Blazor.
```

## 🏁 Como Executar

1.  **Clone o repositório:**
    ```bash
    git clone <URL_DO_SEU_REPOSITORIO>
    cd PatientManagementApp
    ```

2.  **Configure o Backend:**
    Certifique-se de que a API de backend esteja em execução e que a URL base no `PacienteService.cs` esteja correta.

3.  **Execute a aplicação:**
    -   Abra a solução `PatientManagement.App.sln` no Visual Studio.
    -   Selecione o dispositivo ou emulador desejado (ex: Windows Machine, Android Emulator).
    -   Pressione `F5` ou clique no botão "Run" para iniciar a aplicação.

---

*Este README foi gerado com a ajuda do Gemini Code Assist.*
