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

### Bad Request (400)

If you are receiving bad request, then you are trying to submit malformed request. Below is the possible request that can result in 400

```
incorrect json}}{{
```

## POST /oauth/token

This endpoint is used to authenticate existing user. Below is an example of a successful request.

```
POST http://localhost:23806/oauth/token HTTP/1.1
Content-Type: application/x-www-form-urlencoded
Host: localhost:23806
Content-Length: 69

grant_type=password&username=johndoe%40example.com&password=123456aa2
```

Please note that here the content type is `application/x-www-form-urlencoded`. Thus the payload follows the rules of querystring.
There are three parameters - `grant_type`, `username`, `refresh_token` and `password`. 

* *grant_type* - required, can be `password` and `refresh_token`
* *password* - required if grant_type `password` is indicated
* *username* - required if grant_type `password` is indicated
* *refresh_token* - required if grant_type is `refresh_token`

*Please note that email `johndoe@example` in the payload is url encoded i.e. written as `johndoe%40example`. `@` is escaped according to http rules. You must encode your values properly before sending them to the server*

### Success (200)

In case of a successful login, you can receive the following response

```json
{
	"access_token":"epGn6U3t4tnav....mOUMdAXbeJQ",
	"token_type":"bearer",
	"expires_in":43199
}
```

In the above example `access_token` is will contain token that you will be using to access user specific data. `token_type` is always `bearer` and denotes the token type that you will add to the header during authorized requests. `expires_in` are seconds after which the provided token will expire. 


### Bad Request (400)

For security reasons all response will result in status code 400. 

```
{ 
    "error":"invalid",
	"error_description":"Invalid credentials" 
}
```

`error_description` will contain human readable explanation of the error. 

**Please note that this endpoint will block for 5 minutes after three unsuccessful attempts for one username. Once those 5 minutes passes, user can try to login again. However if even next 3 attempts will not be successful, the account will be temporary locked and personal contact of the user will be required to unlock the account**