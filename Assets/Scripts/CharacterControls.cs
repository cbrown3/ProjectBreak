// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CharacterControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterControls"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""2b7a0304-cf19-46bc-9d9c-c9cfe9ccc2a0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""d986d784-8635-43f7-9536-a62dc7b3a6d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f475ce92-a30f-4ce9-adbd-ccd043857ad6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AirDash"",
                    ""type"": ""Button"",
                    ""id"": ""22f119f0-dbae-4d19-8a49-dede3fbb8e41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DirectionalInput"",
                    ""type"": ""Value"",
                    ""id"": ""130dbc91-8acb-4e50-86df-9d9704ed6c70"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light Normal"",
                    ""type"": ""Button"",
                    ""id"": ""f6f509eb-576d-4a06-9c4f-85e1d5685ca9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Heavy Normal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b0c7c1a6-e666-4795-bcf0-f08d18c42131"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light Special"",
                    ""type"": ""Button"",
                    ""id"": ""f3186820-a930-446c-9de4-5737518377a4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heavy Special"",
                    ""type"": ""Button"",
                    ""id"": ""0bb7ac76-b74c-4e61-b588-06a618144936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard"",
                    ""type"": ""Button"",
                    ""id"": ""7334c2e0-962e-4b09-bfec-3b363e42d297"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""5b3d44ed-cc89-4ab1-80ba-7f27ec66cd3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Normal Parry"",
                    ""type"": ""Button"",
                    ""id"": ""83963b68-afc2-46a1-98c4-2f785649bc33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Parry"",
                    ""type"": ""Button"",
                    ""id"": ""7c9faff4-eff0-4b34-bfcb-1edcb3e9e5cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard/Horizontal"",
                    ""id"": ""0ad7297e-56d7-47b5-a80b-ef4a18ebb7ee"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9d628b03-f36c-4a04-9531-b53e4742ac56"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a69beb25-26f3-4d24-b5f3-fa8c8f633e14"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad-Stick/Horizontal"",
                    ""id"": ""cd9a4497-cae4-4cec-b949-e60ef885bec6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e5e563e3-4879-48b6-8dc7-1fded20a2893"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""919e5b2b-2d72-4794-af68-a4f6d7113a53"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad-D-Pad/Horizontal"",
                    ""id"": ""921decd0-161d-46cb-aebf-97b318a97031"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fee59d36-b898-4cef-b67f-620c57d5ae76"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ddc85a3b-8e60-4019-abae-419641a9fdcb"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1e4a0eaa-79ed-4c0e-aea0-e4ace8e7b31e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db1bc2db-a221-4b92-a06a-68456ad5cadb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18c6c4d5-3fb4-438d-8afe-b75621715df2"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e737280e-d72c-46da-a089-da9d50c4d8f1"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light Normal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e50514f0-4e18-4df1-ae52-c6c031002264"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light Normal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard/2DVector"",
                    ""id"": ""e8559e92-2884-4817-9b91-ccdc2add5081"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""57fdc9ee-8f31-470b-b313-631b749b9f59"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""85d4c17d-89cf-4cc6-a885-236cbc972124"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""05392961-6a48-4d49-90c4-eb4189e3ecbc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""749306c0-5f04-45bc-b5c3-80e955fb5f61"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad-Stick/2DVector"",
                    ""id"": ""93f185d3-1e70-4975-b33a-8e0857cf04ba"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9ccbc742-238a-42bc-8a8f-0c69c09fcaf2"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0631b3cf-b7c9-47f1-9aaf-f0955940b557"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ae8b750-7427-4498-861c-ba64bc95ccfe"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9155a25b-9903-4cc1-93fc-58f239c8416e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""GamePad-D-Pad/2DVector"",
                    ""id"": ""341e34be-29e0-4d1b-a36b-26f6535bdcea"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f5f13bd-86d2-43df-86c9-82b9c930d99f"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bbe61757-b3f8-4a9f-b97a-b155d07d2247"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da624744-60c0-4628-a162-8be6fc39fca6"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""596ab4ac-0c2c-4024-9cfa-bcb24b7ab8e3"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionalInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5f18baa3-3fdf-4034-bbdd-58ad7bf14f13"",
                    ""path"": ""<Keyboard>/semicolon"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb84704d-d3af-407e-a947-f60e24b9912f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AirDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff3d69f4-b5ce-4dec-ae5a-d2f0d5c8bfc0"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0296c1fa-50eb-4a83-b0b3-33ff99b31d01"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Guard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e876495b-ef92-4d7b-be4a-009eee1558aa"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66d1e88f-d11e-469b-8f70-d693e84e5311"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f250b6bd-ca7d-45fe-8766-06666957cace"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84680f4e-9d27-449b-8713-03fdd0989706"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Light Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a33d527-41fd-4cf3-92bc-ae14f0806a55"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heavy Normal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4eff7804-3326-4c17-a647-94a330fb5862"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heavy Normal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbfd9f51-82cc-4dd3-be38-4bfdc855adac"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heavy Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df845f77-e49b-46f9-9c8e-1fd78345a1e9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Heavy Special"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""a3748a00-6418-4594-b45d-94497943a244"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""760f7032-c0d4-411b-ab92-8298e76c3796"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""fd408c17-3401-4ea8-b01c-7fc274575a14"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""68f48599-8c9f-4399-b16a-20a37018d39c"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""a5da4721-40e6-41c0-ae58-89ee15cf3954"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""909281f5-2c18-4e8b-a3a7-625bb0aff773"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""e51334b1-b092-4e7b-abcb-f87a3794c4a0"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""193c30b4-5ac2-4ef8-ad4c-477eb9bebddb"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""3f6feee2-7360-46fd-a16b-f8279ce0ee0c"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""308ab2da-72b9-48fb-a285-75d3ec942311"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""a6e155b3-4cd4-481c-805b-85e9fb078ab0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""32ebf71d-3d04-4777-bee5-32f03eb69fbe"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Parry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""4537fedb-a73c-4bb0-b3c2-990383aaf51a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2d13523a-3544-4876-b108-99f96b6a0161"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e67ebd89-f6cd-4d86-adc0-ea2dc6c7a0aa"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default Player 1"",
            ""bindingGroup"": ""Default Player 1"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Default Player 2"",
            ""bindingGroup"": ""Default Player 2"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Move = m_Character.FindAction("Move", throwIfNotFound: true);
        m_Character_Jump = m_Character.FindAction("Jump", throwIfNotFound: true);
        m_Character_AirDash = m_Character.FindAction("AirDash", throwIfNotFound: true);
        m_Character_DirectionalInput = m_Character.FindAction("DirectionalInput", throwIfNotFound: true);
        m_Character_LightNormal = m_Character.FindAction("Light Normal", throwIfNotFound: true);
        m_Character_HeavyNormal = m_Character.FindAction("Heavy Normal", throwIfNotFound: true);
        m_Character_LightSpecial = m_Character.FindAction("Light Special", throwIfNotFound: true);
        m_Character_HeavySpecial = m_Character.FindAction("Heavy Special", throwIfNotFound: true);
        m_Character_Guard = m_Character.FindAction("Guard", throwIfNotFound: true);
        m_Character_Grab = m_Character.FindAction("Grab", throwIfNotFound: true);
        m_Character_NormalParry = m_Character.FindAction("Normal Parry", throwIfNotFound: true);
        m_Character_SpecialParry = m_Character.FindAction("Special Parry", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_Move;
    private readonly InputAction m_Character_Jump;
    private readonly InputAction m_Character_AirDash;
    private readonly InputAction m_Character_DirectionalInput;
    private readonly InputAction m_Character_LightNormal;
    private readonly InputAction m_Character_HeavyNormal;
    private readonly InputAction m_Character_LightSpecial;
    private readonly InputAction m_Character_HeavySpecial;
    private readonly InputAction m_Character_Guard;
    private readonly InputAction m_Character_Grab;
    private readonly InputAction m_Character_NormalParry;
    private readonly InputAction m_Character_SpecialParry;
    public struct CharacterActions
    {
        private @CharacterControls m_Wrapper;
        public CharacterActions(@CharacterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Character_Move;
        public InputAction @Jump => m_Wrapper.m_Character_Jump;
        public InputAction @AirDash => m_Wrapper.m_Character_AirDash;
        public InputAction @DirectionalInput => m_Wrapper.m_Character_DirectionalInput;
        public InputAction @LightNormal => m_Wrapper.m_Character_LightNormal;
        public InputAction @HeavyNormal => m_Wrapper.m_Character_HeavyNormal;
        public InputAction @LightSpecial => m_Wrapper.m_Character_LightSpecial;
        public InputAction @HeavySpecial => m_Wrapper.m_Character_HeavySpecial;
        public InputAction @Guard => m_Wrapper.m_Character_Guard;
        public InputAction @Grab => m_Wrapper.m_Character_Grab;
        public InputAction @NormalParry => m_Wrapper.m_Character_NormalParry;
        public InputAction @SpecialParry => m_Wrapper.m_Character_SpecialParry;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnJump;
                @AirDash.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAirDash;
                @AirDash.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAirDash;
                @AirDash.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAirDash;
                @DirectionalInput.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDirectionalInput;
                @DirectionalInput.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDirectionalInput;
                @DirectionalInput.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnDirectionalInput;
                @LightNormal.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightNormal;
                @LightNormal.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightNormal;
                @LightNormal.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightNormal;
                @HeavyNormal.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavyNormal;
                @HeavyNormal.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavyNormal;
                @HeavyNormal.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavyNormal;
                @LightSpecial.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightSpecial;
                @LightSpecial.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightSpecial;
                @LightSpecial.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnLightSpecial;
                @HeavySpecial.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavySpecial;
                @HeavySpecial.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavySpecial;
                @HeavySpecial.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnHeavySpecial;
                @Guard.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGuard;
                @Guard.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGuard;
                @Guard.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGuard;
                @Grab.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnGrab;
                @NormalParry.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnNormalParry;
                @NormalParry.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnNormalParry;
                @NormalParry.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnNormalParry;
                @SpecialParry.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSpecialParry;
                @SpecialParry.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSpecialParry;
                @SpecialParry.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnSpecialParry;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @AirDash.started += instance.OnAirDash;
                @AirDash.performed += instance.OnAirDash;
                @AirDash.canceled += instance.OnAirDash;
                @DirectionalInput.started += instance.OnDirectionalInput;
                @DirectionalInput.performed += instance.OnDirectionalInput;
                @DirectionalInput.canceled += instance.OnDirectionalInput;
                @LightNormal.started += instance.OnLightNormal;
                @LightNormal.performed += instance.OnLightNormal;
                @LightNormal.canceled += instance.OnLightNormal;
                @HeavyNormal.started += instance.OnHeavyNormal;
                @HeavyNormal.performed += instance.OnHeavyNormal;
                @HeavyNormal.canceled += instance.OnHeavyNormal;
                @LightSpecial.started += instance.OnLightSpecial;
                @LightSpecial.performed += instance.OnLightSpecial;
                @LightSpecial.canceled += instance.OnLightSpecial;
                @HeavySpecial.started += instance.OnHeavySpecial;
                @HeavySpecial.performed += instance.OnHeavySpecial;
                @HeavySpecial.canceled += instance.OnHeavySpecial;
                @Guard.started += instance.OnGuard;
                @Guard.performed += instance.OnGuard;
                @Guard.canceled += instance.OnGuard;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @NormalParry.started += instance.OnNormalParry;
                @NormalParry.performed += instance.OnNormalParry;
                @NormalParry.canceled += instance.OnNormalParry;
                @SpecialParry.started += instance.OnSpecialParry;
                @SpecialParry.performed += instance.OnSpecialParry;
                @SpecialParry.canceled += instance.OnSpecialParry;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Move;
    public struct UIActions
    {
        private @CharacterControls m_Wrapper;
        public UIActions(@CharacterControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_UI_Move;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_DefaultPlayer1SchemeIndex = -1;
    public InputControlScheme DefaultPlayer1Scheme
    {
        get
        {
            if (m_DefaultPlayer1SchemeIndex == -1) m_DefaultPlayer1SchemeIndex = asset.FindControlSchemeIndex("Default Player 1");
            return asset.controlSchemes[m_DefaultPlayer1SchemeIndex];
        }
    }
    private int m_DefaultPlayer2SchemeIndex = -1;
    public InputControlScheme DefaultPlayer2Scheme
    {
        get
        {
            if (m_DefaultPlayer2SchemeIndex == -1) m_DefaultPlayer2SchemeIndex = asset.FindControlSchemeIndex("Default Player 2");
            return asset.controlSchemes[m_DefaultPlayer2SchemeIndex];
        }
    }
    public interface ICharacterActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAirDash(InputAction.CallbackContext context);
        void OnDirectionalInput(InputAction.CallbackContext context);
        void OnLightNormal(InputAction.CallbackContext context);
        void OnHeavyNormal(InputAction.CallbackContext context);
        void OnLightSpecial(InputAction.CallbackContext context);
        void OnHeavySpecial(InputAction.CallbackContext context);
        void OnGuard(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnNormalParry(InputAction.CallbackContext context);
        void OnSpecialParry(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
