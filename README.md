## Tecknologies

    ASP.NET Core 2.1
    C#
    Entity Framework Core
    MSSQL
    LINQ
    RESTful
    Dependence Injection
    CORS

## To start the Contact Manager

    Compile the app to a standalone package (RESTWebApi.zip)
    Unzip standalone file package RESTWebApi.zip to any folder.
    Go to the folder containing the RESTWebApi application.
    Run “ContactManager.exe” to start the Web Api Service. 
    You will see following message shows up:

       Hosting environment: Production
       Content root path: <the folder path>
       Now listening on: http://localhost:5000
       Application started. Press Ctrl+C to shut down.  

##  Database

    Used Entity Framework Core and Code first technologies to initialize database. 

  #  a)	C# Model:

    public class Person
    {
        public const string Customer = "Customer";
        public const string Supplier = "Supplier";

        public long ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [CustomCategoryValidator]
        public string Category { get; set; }

        [StringLength(255)]
        public string Contact { get; set; }

        /* not mapped */
        [NotMapped]
        [CustomEmailValidator]
        public string Email { get; set; }

        [NotMapped]
        [CustomBirthdayValidator]
        public string Birthday { get; set; }

        [NotMapped]
        [CustomTelephoneValidator]
        public string Telephone { get; set; }
    }

  #  b)	Table Person will include following fields:

    ID: Primary Key
    FirstName
    LastName
    Category  (Customer or Supplier) 
    Contact   (Stores Email, Birthday or Telephone) 

  #  c)	Validation includes built-in and customized validators

    Customized validators: 

    CustomCategoryValidator
    CustomEmailValidator
    CustomBirthdayValidator
    CustomTelephoneValidator

  #  d)	The database can be configured to either SQL Server DB or LocalDB (default) in appsettings.production.json

##  Web Api Services

    RESTful Web APIs:

    Get all contacts    :	Get Api/contacts
    Get a page	        :   Get Api/contacts/page/{page number}?size={page size}
    Get a contact	    :   Get Api/contacts/{contact id}
    Create a contact	:   Post Api/contacts
    Update a contact	:   Put Api/contacts
    Delete a contact	:   Delete Api/contacts
            

##	CORS (Cross-Origin Resource Sharing) 

    CORS has been implemented for all origin access.

##	Custom Settings Dependence Injection

    Appsettings.json contains some settings that might be changed on the fly. A CustomSetting model Dependency Injection has been implemented which can be injected into api controller constructor along with DB context DI.
    
##	NLog

    NLog is a useful tool for watching and debugging what had happened in the back-end. It can be enable/disable via changing the appsettings.json.

