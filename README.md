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

* In the above example Password and PasswordConfirmation are the fields containing passwords and they must match. They are required fields.
* Email should be a valid email address and is required as well
* FirstName and LastName are optional.

This is the complete http request with all headers

```
POST http://localhost:23806/api/User/Register HTTP/1.1
Content-Type: application/json
Host: localhost:23806
Content-Length: 159

{
    "Password":"123456aa",
    "PasswordConfirmation": "123456aa",
    "Email": "johndoe@example.com",
    "FirstName": "John",
    "LastName": "Doe"
}
```

*Please note that in the above example you can see the __Content-Type/application/json__ header. It is important, as this header tells server how to read the payload*

### Success (200)

In case of a successful request, response will be as follows

```json
{
    "Data":{
		"Password":"123456aa",
		"PasswordConfirmation":"123456aa",
		"UserId":2,
		"Email":"johndoe@example.com",
		"FirstName":"John",
		"LastName":"Doe"
	},
	"Success":true
}
```

The response will have status code 200, created user in field `Data` and `Success` field denoting the operation result.

### Conflict (409)

In case when the email is duplicate the response code will be 409 and the following will be the response json

```json
{
    "Error":null,
	"StackTrace":null,
	"Data":{
		"Error":"Duplicate email",
		"StackTrace":null,
		"Success":false
	},
	"Success":false
}
```

### Unprocessable Entity (422)

When there are some validation errors, the response code will be 422. For example the following request

```json
{
    "Password":"123",
    "PasswordConfirmation": "123",
    "Email": "johndoe1@example.com",
    "FirstName": "John",
    "LastName": "Doe"
}
```

will result in 

```json
{
    "Error":null,
	"StackTrace":null,
	"Data":{
		"Error":"Password must be 6 characters long",
		"StackTrace":null,
		"Success":false
	},
	"Success":false
}
```

Following is the list of possible response

* *Passwords don't match* - occurs when passwords don't match
* *Password is too weak* - if the password doesn't meet the security requirements
* *Invalid email* - if the provided email is not in a correct format
* *Email is required* - if the email is not provided