
# C# WebApi Best Practices Demo

This project demonstrates key best practices for building robust and scalable C# WebApi applications, covering:

* Project Structure
* Dependency Injection
* Filters
* Repository pattern.
* Environment Configuration (Development and Production).
* JWT Authentication.
* Serilog logging.
* Global Exception Handling.
* API Documentation (Swagger).
* Database Backup Bash Script.

This demo serves as an foundation for developers creating maintainable and production-ready APIs in C#.


## Documentation

For an in-depth guide on each of these topics, see the [detailed documentation](https://agava.kaiten.ru/documents/g/d772877a-0bb5-48da-a046-fee7ee79f80b). This external guide also covers:

* Advanced database management (database models and best practices).
* VPS setup (Docker installation and configuration, nginx setup for HTTPS).
* SignalR and integration with Unity.
* Service deployment on VPS.
* Database logging.


## Database Structure

![App Screenshot](https://github.com/ArtemVetik/demo-webapi/blob/main/docs/images/database_structure.png)


## API Reference

#### Confirm Email

```bash
  POST /api/account/confirm-email
```
Sends a confirmation code to the email.

| Parameter | Type     | Description           |
| :-------- | :------- | :-------------------- |
| `email`   | `string` | **Required**. Target email address |

---

#### Register New User

```bash
  POST /api/account/registration
```
Creates a new user profile.

| Parameter       | Type     | Description                                      |
| :-------------- | :------- | :----------------------------------------------- |
| `email`         | `string` | **Required**. User email                         |
| `password`      | `string` | **Required**. Password                           |
| `name`          | `string` | **Required**. User name                          |
| `gender`        | `int`    | Gender, 70 (Female) or 77 (Male)                 |
| `confirm_code`  | `string` | **Required**. Confirmation code                  |

---

#### Login

```bash
  POST /api/account/login
```
Performs a login and updates the refresh token.

| Parameter       | Type     | Description                     |
| :-------------- | :------- | :------------------------------ |
| `email`         | `string` | User email                      |
| `password`      | `string` | User password                   |

---

#### Refresh Token

```bash
  POST /api/account/refresh
```
Create a new access token using a refresh token.

| Parameter         | Type     | Description         |
| :---------------- | :------- | :------------------ |
| `refresh_token`   | `string` | **Required**. Refresh token |

---

#### Get All Maps

```bash
  GET /api/maps
```
Returns an array of all maps.

---

#### Download Map

```bash
  POST /api/maps/download/{mapId}
```
Provides access to download a map.

**Bearer Token Required**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `mapId`   | `string` | **Required**. Map identifier |

---

#### Upload New Map

```bash
  POST /api/maps/upload
```
Uploading a new map.

**Bearer Token Required**

| Parameter         | Type     | Description                       |
| :---------------- | :------- | :-------------------------------- |
| `name`            | `string` | Name of the map                   |
| `description`     | `string` | Description of the map            |
| `download_url`    | `string` | Download URL for the map file     |

---

#### Get Profile Data

```bash
  GET /api/profile
```
Returns profile data.

**Bearer Token Required**

---
