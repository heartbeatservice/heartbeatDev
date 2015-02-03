CREATE TABLE [dbo].[CustomerInsurance] (
    [CustomerInsuranceID]     INT            IDENTITY (1, 1) NOT NULL,
    [InsuranceID]             INT            NOT NULL,
    [CustomerId]              INT            NOT NULL,
    [EffectiveDate]           DATE           NOT NULL,
    [EndDate]                 DATE           NULL,
    [PCPName]                 NVARCHAR (100) NULL,
    [CustomerInsuranceNumber] NVARCHAR (50)  NULL,
    [InsuranceType]           NVARCHAR (50)  NULL,
    CONSTRAINT [PK_CustomerInsurance] PRIMARY KEY CLUSTERED ([CustomerInsuranceID] ASC)
);



