# UniAPI

**UniAPI** is a unified communication framework designed to simplify and standardize API calls across multiple protocols — **REST**, **GraphQL**, and **gRPC** — using a single, consistent client model.

---

## 🌐 Overview

The goal of this project is to create a client library that allows developers to interact with different service protocols without worrying about their underlying implementations.
Instead of writing separate logic for REST, GraphQL, or gRPC, UniAPI lets you define a single request model that automatically maps to the correct format and handles all the required configurations.

---

## 🚀 Project Roadmap

### **Phase 1 — Unified Client Library**

Develop a unified client that can send requests to REST, GraphQL, and gRPC services using the same request model.

### **Phase 2 — Token & Workflow Management**

Integrate token-based authentication, auto-refresh, and workflow configuration to simplify secured communication between services.

### **Phase 3 — Intelligent Caching System**

Add multi-level caching (in-memory, Redis, etc.) with protocol-aware logic to reduce redundant network calls and improve performance.

### **Phase 4 — Adaptive Protocol Detection**

Enable the system to automatically detect and switch to the appropriate protocol (REST / GraphQL / gRPC) based on the target service’s capabilities — removing the need for manual protocol declaration.

---

## 🧩 Example Concept

```csharp
var request = new UniRequest<UserData>
{
    Protocol = ApiProtocol.Rest,
    BaseUrl = "https://api.example.com",
    PathOrQuery = "/users/123",
    Body = new UserData { Name = "John" }
};
```

Or switch to GraphQL or gRPC — **without changing your client code structure**.

---

## 💡 Vision

UniAPI aims to bridge the gap between different communication standards in distributed systems, helping developers focus on business logic rather than protocol details.

---

## 🛠️ Future Add-ons

* Smart retry and resilience policies
* Built-in metrics and logging
* Integration with service discovery systems
* Automatic serialization and compression optimization

---

## 📄 License

This project is open-source and distributed under the MIT License.
