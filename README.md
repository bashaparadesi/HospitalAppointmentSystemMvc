🏥 Hospital Appointment System (ASP.NET MVC)

A web-based hospital appointment booking system that enables patients to register, log in, browse doctors, and book appointments seamlessly.

🚀 Features
👤 Patient Management

Patient registration and authentication

Secure login with session management

Logout functionality

🩺 Doctor Management

View available doctors

Doctor specialization listing

📅 Appointment Booking

Select doctor, date, and time

Confirm booking

Automated mapping of PatientID and DoctorID

Appointment slip generation (print/download)

🧾 Appointment History

View all booked appointments

Appointment status display

Print appointment slip for each booking

🛠️ Tech Stack
Layer	Technology
Frontend	ASP.NET MVC, Bootstrap, Razor
Backend	C#, ADO.NET, Repository Pattern
Database	SQL Server (Stored Procedures)
Authentication	Sessions
IDE	Visual Studio
Hosting	IIS / Azure (optional)
🗂️ Project Structure
Controllers/
  - AppointmentController.cs
  - PatientController.cs
  - HomeController.cs

DAL/
  - HospitalDAL.cs

Models/
  - Patient.cs
  - Doctor.cs
  - Appointment.cs

Views/
  - Shared/_Layout.cshtml
  - Appointment/*.cshtml
  - Patient/*.cshtml
  - Home/*.cshtml

📊 Database & Stored Procedures

The application uses SQL Server stored procedures:

sp_GetDoctors — Get all doctors

sp_RegisterPatient — Register new patient

sp_LoginPatient — Login authentication

sp_BookAppointment — Insert appointment

sp_GetAppointmentsByPatient — Get appointments for logged-in user

🔐 Authentication & Sessions

Patients must log in to book appointments

Session stores:

PatientID

PatientName

If not logged in → redirect to login page

📃 Appointment Slip

After successful booking, a slip is displayed:

Patient Name

Doctor ID

Date & Time

Status

Print / Download option
