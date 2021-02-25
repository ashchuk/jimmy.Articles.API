## Domain

Every domain entity separately stored in it's folder.

Each folder contains:
- Commands - an operations that changes data or state. This folder contains command and command handler implementation; 
- and Queries - an operations that not changes data but makes data queries and retrieve in to app user. This folder contains command and command handler implementation;

This directory structure make it handy to add new entities and divide it`s logics to individual part.