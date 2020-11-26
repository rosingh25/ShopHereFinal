# ShopHere
Entity Framework Version : v6.2.0     <br/>
Bootstrap Version : v4.5.3            <br/>
Note : if enable-migrations does not work.
  1. right click on project name -> Manage NuGet packages
  2. Search EntityFramework and install v6.2.0 in the project



Steps to to download and load project in visual studio:
  1. Delete Migrations folder from project after downloading project.
  2. Go to Tools -> NuGet Package Manager -> Package Manager Console.
  3. If warning message at top of package manager console : similar to - some packages are missing : restore
  4. if(above warning) click on restore.
  5. In Package Manager Console -> Run the below No. 6 & 7 commands.
  
  6. enable-migrations.
  7. add-migration FirstMigration.
  
  8. At the end of inside Up() method in 2020..._ FirstMigration of Migrations folder, paste -> Pending Migration File.txt content of PendingMigration folder and save the files.   
  9. Delete App_Data folder from project, right click on project -> Add Asp.Net folder -> App_Data.
  
  10. in Package Manager Console -> run below No. 11 command.
  
  11. update-database 
  
  12. Now run the project using control + F5.
  13. If it shows risk to enter website -> go to Advanced button -> Visit Anyways.    <br/>
  
  <br/>
  If Still Errors in packages:      <br/>
  
  Tools > NuGet Package Manager > Package manager setting >. Under General, select Allow NuGet to download missing packages.        <br/>
  
  In Solution Explorer, right click the solution and select Restore NuGet Packages.

Functionality:
 ----------------------
  For first time user to interact with website UI following are the instructions:
  1. Register button at navbar to register.
  2. At end of register form option to register as Admin.
  3. Login button to register at navbar to login (common login form for both admin and customer).
  4. Admin can add, edit or delete items added by him as well as can see orders by customers of items added by him.
  5. customer can buy items or view his orders.
  6. Functionality of searching and filter items is provided in application also.
  7. Pagination is also added to have a better interaction with UI (Max 4 items per page). <br />
Note: There can be >1 admins registered and each admin will have control on his own products and can't modify any other Admin's added items.<br/>

ShopHere Web Application to allow Customers to shop + Manage data as Admin
  1. Any Issues found : please mail to chambiyalrohit344@gmail.com.

Upcoming Changes :
  No Upcoming Changes right now.

Changes : 
23-11-2020
  1. Added Admin Login
  2. Restricted Admin Access to his own products
  3. Custom Error Added
  4. Allow Anonymous Customer Search
  5. Resolved Edit Item in view admin name missing issue
  6. Resolved Search for customer only 1 page issue.
  7. Extended filter capability to search.
  
22-11-2020
  1. Added Order Model
  2. Added Address property in User- 3.IdentityModels
  4. Updated Views for Addition Deletion Editing or item
  5. configured routing of all missing links in views and controllers
  6. Added Order Controller and added order viewing actions to it
  7. Added search by orderit in view orders
  8. Updated search item action.


  
21-11-2020
  1. Added Categories to Items
  2. Added Categories data through migrations
  3. Worked on Layout, AddingItem View
  4. Implemented pagination.
  
  
20-11-2020
  1. Added Filter for Authorize
  2. Created AdminController
  3. Deleted few Shopping Actions
  4. Added RegisterAdmin for Admin Role in AccountController.
  5. Admin Layout. 
  6. Added RegisterUser for Normal Role in AccountController.
  
19-11-2020
  1. Index Shopping Page
  2. Buy Item Action 
  3. Updated Bootstrap Version
  4. Added Homepage View
  5. Implemented Searching : SearchItemByCustomer Action in Shopping Controller.
  6. Applied BootStrap In HomePage & Layout for Front End Development.



18-11-2020 
  1. enabled Entity Framework
  2. Created Item Model
  3. Created Table of Item Mode in Database using DbSet.
  4. Created Shopping Controller with -
      a. AddItemInView, AddItemToDb actions and Views created.
  5. ViewAllItems
  6. AddItemInView
  7. AddItemToDb
  8. EditItemInView
  9. EditItemInDb
  10. DeleteItemFromDb
  11. Added Annotations in Item model
    (Added above Actions and their corrosponding views in Shopping Controller:)
