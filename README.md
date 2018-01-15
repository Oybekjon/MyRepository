# API Documentation

This is a documentation of an API

The following is the request to create a user. Method is POST and url is /api/User/Register. This endpoint accepts the payload as follows.

## POST /api/User/Register
```json
{
    "Password":"123456aa",
    "PasswordConfirmation": "123456aa",
    "Email": "johndoe@example.com",
    "FirstName": "John",
    "LastName": "Doe"
}
```

In the above example Password and PasswordConfirmation are the fields containing passwords and they must match. They are required fields.
Email should be a valid email address and is required as well
FirstName and LastName are optional.