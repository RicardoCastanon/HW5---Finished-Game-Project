﻿// Part 1: Data Model Design with Records and Discriminated Unions.

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

// Part 2: Initialize the game world

// --------- Initial World ---------
let key: Item =
    { Details =
        { Name = "A shiny key"
          Description = "This key looks like it could open a nearby door." } }

let allRooms =
    [

      { Id = RoomId "center"
        Details =
          { Name = "A central room"
            Description =
              "You are standing in a central room with exits in all directions.  A single brazier lights the room." }
        Items = []
        Exits =
          { North = PassableExit("You see a darkened passageway to the north.", RoomId "north1")
            South = PassableExit("You see door to the south.  A waft of cold air hits your face.", RoomId "south1")
            East =
              LockedExit(
                  "You see a locked door to the east.",
                  key,
                  PassableExit("You see an open door to the east.", RoomId "east1")
              )
            West = PassableExit("You see an interesting room to the west.", RoomId "west1") } }

      { Id = RoomId "north1"
        Details =
          { Name = "A dark room"
            Description =
              "You are standing in a very dark room.  You hear the faint sound of rats scurrying along the floor." }
        Items = []
        Exits =
          { North = NoExit None
            South = PassableExit("You see an dimly lit room to the south.", RoomId "center")
            East = NoExit None
            West = NoExit None } }

      { Id = RoomId "south1"
        Details =
          { Name = "A cold room"
            Description =
              "You are standing in a room that feels very cold.  Your breath instantly turns into a white puff." }
        Items = []
        Exits =
          { North = PassableExit("You see an exit to the north.  That room looks much warmer.", RoomId "center")
            South = NoExit None
            East = NoExit None
            West = NoExit None } }

      { Id = RoomId "west1"
        Details =
          { Name = "A cozy room"
            Description =
              "This room seems very cozy, as if someone had made a home here.  Various personal belongings are strewn about." }
        Items = [ key ]
        Exits =
          { North = NoExit None
            South = NoExit None
            East = PassableExit("You see a doorway back to the lit room.", RoomId "center")
            West = NoExit None } }

      { Id = RoomId "east1"
        Details =
          { Name = "An open meadow"
            Description =
              "You are in an open meadow.  The sun is bright and it takes some time for your eyes to adjust." }
        Items = []
        Exits =
          { North = NoExit None
            South = NoExit None
            East = NoExit None
            West =
              PassableExit("You see stone doorway to the west.  Why would you want to go back there?", RoomId "center") } } ]

let player: Player =
    { Details =
        { Name = "Luke"
          Description = "Just your average adventurer." }
      Inventory = []
      Location = RoomId "center" }

//  gameWorld includes a map of rooms and a player(you need to convert the room list to room map)

let gameWorld: World =
    { Rooms = allRooms |> Seq.map (fun room -> (room.Id, room)) |> Map.ofSeq
      Player = player }
