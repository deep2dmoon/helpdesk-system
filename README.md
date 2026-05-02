## Project Overview

- This is an helpdesk system for a B2C Organization , it support the creation and Resolve of Tickets ( Issues , Complaints or Enquiries) between the Customers and the Business (Administrators). It Allows the real time chat and the upload of Ticket related dcouments or files.

## Architectural Design Approach
- A Monolithic architecturing design approach was adopted, in which every components shared the same infrastucture and are tightly coupled into a deployable single codebase unit.

### System Components

- Authentication and Role-Based Authorization
- Notification Service
- Database Infrastructure
- Messenger 
- Core Helpdesk Service
- File Upload Handler
- Guest and Protected API Endpoints


### Data Transfer And LifeCycle
- Every Ticket data is created by the User / Customer and received by the administrators and ticket is updated by the Customer and Admininstrator , and It can only be closed/ Ended ny the administratore. Ticket remian open until the the attendant (Admin) closes it, and upon closed , every update actions are revolted.

- Create => Update => Update => Close.

### How it works

- User Sign in/ Sign up into the helpdesk system.
- Create new ticket (into Pending State) , Admin receives the notification
- Admin Opens the ticket and Issuer get notified of the action, ticket State changes to Open;
- Tickets get update with conversations and related files upload
- Admin finalises and Close Ticket 

### API Endpoints
[Login Endpoint](http://localhost:PORT/api/auth/login) [Email & Password]
[Sign Up](http://localhost:PORT/api/auth/signup) [Fullname , Email, Password]
[Create Ticket](http://localhost:PORT/api/helpdesk/new/ticket")
[Open Ticket](http://localhost:5299/api/helpdesk/admin/admin@info.co/open/ticket/ticketID)
[Open Close](http://localhost:5299/api/helpdesk//admin/close/ticket/ticketID)
[File Upload](http://localhost:5299/api/helpdesk/ticket/ticketID/file/upload)
[Messaging](http://localhost:5299/api/helpdesk/send/message)


 - payload = {
    "TicketID":"ba827fce583d46cf",
    "Sender":"user@account.com",
    "Response":"User message here!"
}

[Guest Read Ticket](http://localhost:5299/api/helpdesk/guest/all/tickets) [Reserved Tickets Properties e.g Category,  and- Conversation]
