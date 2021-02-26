## Domain

Every domain entity is separately stored in its folder.

Each folder contains:
- Commands - an operation that changes data or state. This folder contains command and command handler implementation;
- and Queries - an operation that not changes data but makes data queries and retrieves it for an app user. This folder contains command and command handler implementation;

This directory structure makes it handy to add new entities and divide their logics into individual parts.
