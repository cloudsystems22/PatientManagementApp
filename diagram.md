graph TD
    subgraph "PatientManagement.App (Cliente Nativo)"
        A("Interface do Usuário<br/>Páginas Blazor")
        B("Serviços de Aplicação<br/>PacienteService.cs")
        C("Componentes Nativos<br/>.NET MAUI")
    end

    subgraph "Backend"
        D("API REST<br/>PatientManagement.Api")
        E[("Banco de Dados")]
    end

    A -->|Interação do Usuário| B
    B -->|Requisições HTTP| D
    D -->|Acessa dados| E
    D -->|Respostas JSON| B
    B -->|Atualiza estado| A
    C -->|Hospeda e integra recursos| A
