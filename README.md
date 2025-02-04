# Health Assured Checkout Kata

<img width="601" alt="image" src="https://github.com/JabranShaheen/BrightHR/assets/34131257/c2149a30-2be3-4d1e-809a-8fafe02cb520">

# Development Approach
## Test-Driven Development (TDD)
The Checkout System follows the Test-Driven Development (TDD) methodology throughout the development process. This approach involves writing tests before writing the actual implementa-tion code, ensuring that the code meets the specified requirements and remains maintainable in the long run. The test suite covers various scenarios to validate the correctness and robustness of the system.

## Clean Architecture
The architecture of the Checkout System adheres to clean architecture principles to maintain sepa-ration of concerns and ensure high modularity. The architecture comprises distinct layers:

### Entities:
Represent the core domain entities in the system, such as items and promotions.
### Services: 
Provide business logic and operations for interacting with entities.
### Abstractions: 
Define interfaces for services to promote loose coupling and enable dependency in-jection.

# SOLID Principles
The Checkout System is designed with adherence to SOLID principles, focusing particularly on the Single Responsibility Principle (SRP). Each service in the system has a single responsibility, encapsulating one aspect of the system's functionality. For example:

ItemService: Manages CRUD operations for items.
PromotionService: Handles the management of promotions.
CheckoutService: Facilitates the checkout process.

## Abstractions
The system utilizes interfaces to promote Interface Segregation Principle (ISP). Abstractions are defined for each service, ensuring that interfaces contain only the methods that are relevant to the implementing classes. This promotes loose coupling and ensures that clients are not forced to depend on interfaces they do not use.

## Dependency Inversion
Dependency inversion is achieved by ensuring that high-level modules (services) do not depend on low-level modules; both depend on abstractions. This promotes loose coupling and facilitates easier maintenance and testing. All services depend on abstractions, ensuring that they are not tightly coupled to each other.

# Components
## Entities
**Item:** Represents a product in the store, characterized by a unique SKU (Stock Keeping Unit) and a unit price.

**Promotion:** Defines special offers or discounts applicable to specific items or combinations of items.

## Services
**ItemService:** Manages the CRUD (Create, Read, Update, Delete) operations for items.

**PromotionService:** Handles the management of promotions, including adding, retrieving, and ap-plying them to items during checkout.

**CheckoutService:** Facilitates the checkout process, allowing users to scan items, apply promotions, and calculate the total price.

# Testing Strategy
The testing strategy focuses on ensuring the correctness, reliability, and maintainability of the sys-tem. There is a comprehensive suite of unit tests that cover various aspects of the system, includ-ing:
Unit tests for individual services.
