// Part 1: Data Model Design with Records and Discriminated Unions.

// --------- Model ---------
type Details = { Name: string; Description: string }

type Item = { Details: Details }

type RoomId = RoomId of string

// discriminated unions

type Exit =
    | PassableExit of string * destination: RoomId
    | LockedExit of string * key: Item * next: Exit
    | NoExit of string option

type Exits =
    { North: Exit
      South: Exit
      East: Exit
      West: Exit }

type Room =
    { Id: RoomId
      Details: Details
      Items: Item list
      Exits: Exits }

type Player =
    { Details: Details
      Location: RoomId
      Inventory: Item list }

type World =
    { Rooms: Map<RoomId, Room>
      Player: Player }
