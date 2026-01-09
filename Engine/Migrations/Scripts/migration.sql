CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "PhoneNumber" text NOT NULL,
    "Address" text NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

CREATE TABLE "HelpRequests" (
    "Id" uuid NOT NULL,
    "SeekerId" uuid NOT NULL,
    "HelperId" uuid,
    "Description" text NOT NULL,
    "Status" text NOT NULL,
    "Price" numeric NOT NULL,
    "IsPaid" boolean NOT NULL,
    "CompletedAt" timestamp with time zone,
    CONSTRAINT "PK_HelpRequests" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_HelpRequests_Users_HelperId" FOREIGN KEY ("HelperId") REFERENCES "Users" ("Id"),
    CONSTRAINT "FK_HelpRequests_Users_SeekerId" FOREIGN KEY ("SeekerId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "TravelPlans" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "FromCountry" text NOT NULL,
    "ToCountry" text NOT NULL,
    "TravelDate" timestamp with time zone NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_TravelPlans" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_TravelPlans_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ChatMessages" (
    "Id" uuid NOT NULL,
    "HelpRequestId" uuid NOT NULL,
    "SenderId" uuid NOT NULL,
    "Message" text NOT NULL,
    "SentAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_ChatMessages" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ChatMessages_HelpRequests_HelpRequestId" FOREIGN KEY ("HelpRequestId") REFERENCES "HelpRequests" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_ChatMessages_HelpRequestId" ON "ChatMessages" ("HelpRequestId");

CREATE INDEX "IX_HelpRequests_HelperId" ON "HelpRequests" ("HelperId");

CREATE INDEX "IX_HelpRequests_SeekerId" ON "HelpRequests" ("SeekerId");

CREATE INDEX "IX_TravelPlans_UserId" ON "TravelPlans" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260109043213_InitialCreate', '8.0.0');

COMMIT;

