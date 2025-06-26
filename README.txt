The Hospital Reservation System is a web-based application developed to manage hospital operations such as appointment scheduling, user roles, patient and doctor information and department tracking. The system is built using C#, the ASP.NET Core MVC, and SQLite for backend database management. It provides different interfaces and privileges for three types of users: Admin, Doctor, and Patient. The main goal is to digitalize and automate hospital services to improve accessibility, organization, and efficiency for both medical staff and patients.

In a real-world hospital setting, administrative and scheduling tasks are often managed manually or via outdated systems, leading to inefficiencies, miscommunications, and delays in patient care. Our Hospital Reservation System simulates a digital transformation initiative where a hospital seeks to modernize its internal operations. The system allows patients to book, edit, or cancel appointments with doctors, while doctors can view and manage their scheduled visits. Administrators have full control over user management, doctor and patient records, department listings, and more.

Based on our analysis and simulated client expectations, the following key requirements were defined for the project:
•	A Login Screen for secure user authentication with role-based access (Admin, Doctor, Patient).
•	Doctor Management features: add, edit, delete, and list doctors.
•	Patient Management features: add, edit, delete, and list patients.
•	Appointment Reservation functionality: schedule, view, reschedule, or cancel appointments.
•	Department Management: full CRUD operations for medical departments like Cardiology or Neurology.
•	User Management (Admin-only): add, edit, delete users and assign roles.
•	Use of modern technologies: C#, ASP.NET Core MVC or Blazor, and SQL Server, optionally styled with Bootstrap.



Users:

For admin role:

info@emreocak.com
Admin123123
------------------
For doctor role:

info@stephencurry.com
123123

kobe@kobe.com
123123
------------------
For patient role:

vawati5402@neuraxo.com
1231234

antony@antony.com
123123

---------------------------------
There are 6 doctors. The last 2 ones(Stephen Curry, Kobe Bryant) was created as real users and assigned as doctors.
In those accounts, you can see the appointments that created by patients, if the patients made their appointments from these doctors.
The other doctors were created as examples and they do not have accounts.
In the contact us page, submit button is not functional, and was created as an example.
You can add doctors in the admin panel, but if you want doctors to be real accounts, you register the page first and then assign doctor role in the admin panel. It automatically makes the account doctor and list the user as a doctor.
