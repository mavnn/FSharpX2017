open Freya.Machines
open Freya.Machines.Http.Machine.Configuration
open Freya.Routers

let users =
    freyaMachine {
        methods [ GET; OPTIONS; POST ]
        availableMediaTypes MediaType.json
        doPost createUser
        handleOk listUsers }

let routes =
    freyaRouter {
        route GET "/" home
        resource  "/api/users" users
        resource  "/api/users/{id}" user }
