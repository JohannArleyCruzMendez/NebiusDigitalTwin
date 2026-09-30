# 🛡️ Digital Twin Security Center (NebiusDigitalTwin)

An enterprise-grade **Digital Twin Security Simulation Platform** built with **.NET 8**, **Clean Architecture**, and an **AI-powered resiliency engine** utilizing **Nebius Token Factory (NVIDIA Nemotron)** as the primary LLM provider and **Groq** as an automated high-availability fallback.

---

## 🏗️ Architecture & Tech Stack

The solution follows strict **Clean Architecture** principles separated into modular layers:
* **`NebiusDigitalTwin.Domain`**: Core entities, value objects, and domain logic for network nodes, asset criticalities, and security postures.
* **`NebiusDigitalTwin.Application`**: CQRS pattern implemented via **MediatR**, managing commands and handlers for asset registration and threat simulations.
* **`NebiusDigitalTwin.Infrastructure`**: External integrations, MongoDB repositories, and resilient AI multi-provider services.
* **`NebiusDigitalTwin.API`**: RESTful Web API controllers exposing endpoints for digital twin management and LLM orchestration.
* **`NebiusDigitalTwin.Web`**: ASP.NET Core Razor Pages / MVC frontend featuring dynamic interactive UI, network topology mapping (`vis-network`), and real-time telemetry badges.

---

## ⚡ AI Resiliency & Multi-Provider Strategy

To ensure zero downtime during critical security assessments, the platform features an automated fallback mechanism:
1. **Primary Provider**: **Nebius Token Factory** powered by NVIDIA's high-performance model (`nvidia/NVIDIA-Nemotron-3-Nano-30B-A3B`).
2. **Fallback Provider**: **Groq AI** configured for instant failover handling if token limits or network timeouts occur.
3. **Real-time Telemetry UI**: Dynamic frontend status badges update instantly (`Green` for Nebius Active, `Yellow` for Groq Fallback).

---

## 🚀 Getting Started & Quickstart

### Prerequisites
* **.NET 8 SDK** installed on your machine.
* **MongoDB** running locally (`mongodb://localhost:27017`) or via Docker.

### 1. Clone the Repository
```bash
git clone [https://github.com/your-username/NebiusDigitalTwin.git](https://github.com/your-username/NebiusDigitalTwin.git)
cd NebiusDigitalTwin
2. Configure appsettings.json
Navigate to your API / Web configuration and set up your database connection and secure API keys:

JSON
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "NebiusDigitalTwinDb",
    "CollectionName": "Nodos"
  },
  "AiProviders": {
    "Nebius": {
      "BaseUrl": "[https://api.tokenfactory.nebius.com/v1/](https://api.tokenfactory.nebius.com/v1/)",
      "ApiKey": "YOUR_NEBIUS_API_KEY",
      "Model": "nvidia/NVIDIA-Nemotron-3-Nano-30B-A3B"
    },
    "Groq": {
      "BaseUrl": "[https://api.groq.com/openai/v1/](https://api.groq.com/openai/v1/)",
      "ApiKey": "YOUR_GROQ_API_KEY",
      "Model": "qwen/qwen3.8-27b"
    }
  }
}
3. Run the Application
Start both the backend API and the web interface from your terminal or Visual Studio:

Bash
dotnet run --project NebiusDigitalTwin.API
dotnet run --project NebiusDigitalTwin.Web
📌 Core Features
Asset Inventory Digital Twin: Register infrastructure nodes (Web Servers, Database Servers, Firewalls, Wireless Access Points) with custom CVE vulnerabilities and network segmentation properties.

AI Threat Vector Simulation: Input natural language attack scenarios (e.g., Lateral movement attack exploiting web backend to reach core database) to receive global risk levels, exact attack paths, and prioritized mitigations.

Interactive Topology Mapping: Visualizes high-risk assets and network connections.