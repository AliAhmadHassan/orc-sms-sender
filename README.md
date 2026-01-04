# OrcSMS Sender

Batch SMS preparation tool used by Orcozol to organize outbound messages for clients. This solution focuses on managing SMS batches, persisting message data, and supporting Excel-based import/export for operational workflows.

## Why this project matters
- Built for reliability in high-volume, repetitive operations where speed and consistency matter.
- Clean separation of concerns makes it maintainable and easy to extend.
- Uses stored procedures and DTO metadata to keep data access predictable and auditable.

## Highlights
- Multi-layer architecture: WinForms UI + Business Logic + Data Access + DTOs.
- Reflection-driven CRUD mapped to stored procedures through attributes.
- Excel Interop pipeline for bulk import/export of message data.
- Multiple database connections for operational and reporting data sources.

## Architecture
The solution is split into four projects:
- `OrcSMS.WFApp`: WinForms desktop shell for operators.
- `OrcSMS.BLL`: business services that expose CRUD workflows for domain entities.
- `OrcSMS.DAL`: ADO.NET data access and generic repository logic.
- `OrcSMS.DTO`: data transfer objects and metadata attributes.

Core flow:
1) UI calls BLL services.
2) BLL delegates to DAL.
3) DAL executes stored procedures, using DTO attributes to map parameters and keys.

## Domain model
Key DTOs:
- `Remessa`: a batch of messages (campaign or send group).
- `Mensagem`: an individual SMS message (DDD, phone, text, status, batch).
- `MensagemStatus`: delivery/status enum table.

These DTOs define stored-procedure names through `AtributoBind`, which the DAL reads at runtime.

## Excel integration
The `XLSX` helper supports:
- Exporting DTO lists to an Excel template.
- Importing a sheet into DTOs based on column/row metadata.

Column mapping is declared via attributes in `OrcSMS.DTO.XLSX_*`, allowing structured batch templates without hardcoded column indexes.

## Tech stack
- C# and .NET Framework 4.5
- WinForms desktop UI
- ADO.NET + SQL Server stored procedures
- Microsoft Office Interop for Excel

## Setup (developer)
1) Open `OrcSMS.sln` in Visual Studio (2013+).
2) Configure connection strings in `OrcSMS.WFApp\App.config`:
   - `ConnectionStringOrcSMS`
   - `ConnectionStringCobNet`
   - `ConnectionStringControleAcesso`
3) Ensure the SQL Server database has these procedures:
   - Remessa: `SPIRemessa`, `SPURemessa`, `SPDRemessa`, `SPSRemessa`, `SPSRemessaPelaPK`
   - Mensagem: `SPIMensagem`, `SPUMensagem`, `SPDMensagem`, `SPSMensagem`, `SPSMensagemPelaPK`
   - MensagemStatus: `SPIMensagemStatus`, `SPUMensagemStatus`, `SPDMensagemStatus`, `SPSMensagemStatus`, `SPSMensagemStatusPelaPK`
4) Fix the Excel Interop reference in `OrcSMS.DAL` if needed (current hint path is a local network share).
5) Build and run `OrcSMS.WFApp`.

## Notes and limitations
- This repository focuses on data preparation and persistence; the actual SMS gateway integration is handled elsewhere.
- Excel Interop requires Office installed on the machine running the import/export.
- App.config in this repo is a placeholder; connection strings are required to run.

## What I would improve next
- Replace Excel Interop with a server-safe library (e.g., OpenXML) for headless processing.
- Add validation and preview steps before persisting batches.
- Expand the UI to cover the full message lifecycle with dashboards and filtering.
- Add unit tests around the reflection-based mapping and stored-procedure contracts.
