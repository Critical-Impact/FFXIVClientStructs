using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Network;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Common.Component.Excel;
using FFXIVClientStructs.FFXIV.Component.Text;

namespace FFXIVClientStructs.FFXIV.Client.UI.Agent;

// Client::UI::Agent::AgentLobby
//   Client::UI::Agent::AgentInterface
//     Component::GUI::AtkModuleInterface::AtkEventInterface
// ctor "E8 ?? ?? ?? ?? EB 03 48 8B C5 45 33 C9 48 89 47 20"
[Agent(AgentId.Lobby)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x1E68)]
[VirtualTable("48 8D 05 ?? ?? ?? ?? 48 89 69 ?? 48 89 01 4C 8B E1", 3)]
public unsafe partial struct AgentLobby {
    [FieldOffset(0x40)] public LobbyData LobbyData; // for lack of a better name
    
    // TODO: everything below here is wrong
    //[FieldOffset(0xA00)] public UIModule* UIModule;
    //[FieldOffset(0xA08)] internal nint TitleScreenMoviePtr;

    [FieldOffset(0xA30)] public uint AccountExpansion;
    [FieldOffset(0xA34)] public bool ShowFreeTrialLogo;

    [FieldOffset(0xA38)] public uint TitleScreenExpansion;
    [FieldOffset(0xA3C)] public bool ShowOriginalLogo; // pre-relaunch

    [FieldOffset(0xA40)] public ExcelSheet* ErrorSheet;
    [FieldOffset(0xA48)] public ExcelSheet* LobbySheet;
    [FieldOffset(0xA50)] public NetworkModuleProxy* NetworkModuleProxy;
    [FieldOffset(0xA58)] public StdDeque<TextParameter> LobbyTextParameters;
    [FieldOffset(0xA80), FixedSizeArray] internal FixedSizeArray13<Utf8String> _tempUtf8Strings;

    [FieldOffset(0x10E0)] public sbyte ServiceAccountIndex;
    [FieldOffset(0x10E1)] public sbyte HoveredCharacterIndex; // index in CharaSelectCharacterList
    [FieldOffset(0x10E8)] public ulong HoveredCharacterContentId;
    [FieldOffset(0x10F0)] public byte DataCenter;

    [FieldOffset(0x10F2)] public short WorldIndex; // index in CurrentDataCenterWorlds
    [FieldOffset(0x10F4)] public ushort WorldId;

    [FieldOffset(0x10F8)] public uint DialogAddonId;
    [FieldOffset(0x10FC)] public uint DialogAddonId2;
    [FieldOffset(0x1100)] public uint LobbyScreenTextAddonId;

    [FieldOffset(0x1104)] public byte LobbyUpdateStage;

    [FieldOffset(0x1107)] public byte LobbyUIStage;

    [FieldOffset(0x1110)] public uint IdleTime;

    [FieldOffset(0x1138)] public ulong SelectedCharacterContentId;
    [FieldOffset(0x1140)] public bool IsLoggedIn;

    [FieldOffset(0x1228)] public bool TemporaryLocked; // "Please wait and try logging in later."

    [FieldOffset(0x1240)] public long RequestContentId;
    [FieldOffset(0x1248)] public byte RequestCharaterIndex;
    [FieldOffset(0x1DA4)] public bool HasShownCharacterNotFound; // "The character you last logged out with in this play environment could not be found on the current data center."

    [MemberFunction("E8 ?? ?? ?? ?? 48 8D 8E ?? ?? ?? ?? 41 8B D5")]
    public partial void UpdateLobbyUIStage();

    [MemberFunction("E8 ?? ?? ?? ?? 84 C0 74 07 C6 86 ?? ?? ?? ?? ?? 48 8B 8C 24")]
    public partial void UpdateCharaSelectDisplay(sbyte index, bool a2);

    [MemberFunction("E8 ?? ?? ?? ?? C6 87 ?? ?? ?? ?? ?? 66 C7 87 ?? ?? ?? ?? ?? ??")]
    public partial void OpenLoginWaitDialog(int position);
}

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x9E0)]
public unsafe partial struct LobbyData {
    [FieldOffset(0)] public AgentLobby* AgentLobby;
    [FieldOffset(0x8)] public LobbyUIClient LobbyUIClient;

    [FieldOffset(0x878)] public StdVector<Pointer<CharaSelectCharacterEntry>> CharaSelectEntries;

    [FieldOffset(0x898)] public ulong ContentId;
    [FieldOffset(0x8A0)] public Utf8String HomeWorldName;
    [FieldOffset(0x908)] public Utf8String HomeWorldName2;
    [FieldOffset(0x970)] public Utf8String CurrentWorldName;

    [FieldOffset(0x9DC)] public ushort CurrentWorldId;
    [FieldOffset(0x9DE)] public ushort HomeWorldId;

    [MemberFunction("E8 ?? ?? ?? ?? 84 C0 74 19 C6 87 ?? ?? ?? ?? ??")]
    public partial CharaSelectCharacterEntry* GetCharacterEntryFromServer(byte index, ulong contentId);

    [MemberFunction("40 53 48 83 EC 20 49 63 D9 E8 ?? ?? ?? ??")]
    public partial CharaSelectCharacterEntry* GetCharacterEntryByIndex(int a2, int worldIndex, int characterMappingIndex);
}

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x6F8)]
public unsafe partial struct CharaSelectCharacterEntry {
    [FieldOffset(0x8)] public ulong ContentId;
    [FieldOffset(0x10)] public byte Index;
    [FieldOffset(0x11)] public CharaSelectCharacterEntryLoginFlags LoginFlags;

    [FieldOffset(0x18)] public ushort CurrentWorldId;
    [FieldOffset(0x1A)] public ushort HomeWorldId;

    [FieldOffset(0x2C), FixedSizeArray(isString: true)] internal FixedSizeArray32<byte> _name;
    [FieldOffset(0x4C), FixedSizeArray(isString: true)] internal FixedSizeArray32<byte> _currentWorldName;
    [FieldOffset(0x6C), FixedSizeArray(isString: true)] internal FixedSizeArray32<byte> _homeWorldName;
    [FieldOffset(0x8C), FixedSizeArray(isString: true)] internal FixedSizeArray1024<byte> _rawJson;

    [FieldOffset(0x4A0)] public StdVector<Pointer<CharaSelectRetainerInfo>> RetainerInfo;

    [FieldOffset(0x4C0)] public CharaSelectCharacterInfo CharacterInfo; // x2?

    [MemberFunction("0F B6 41 ?? 84 05 ?? ?? ?? ?? 0F 94 C0")]
    public partial bool IsNotLocked();

    [MemberFunction("E8 ?? ?? ?? ?? 84 C0 75 56 48 8B CB")]
    public partial bool IsInDifferentRegion();
}

// see "E8 ?? ?? ?? ?? 44 0F B6 43 ?? 8D 57 7E"
public enum CharaSelectCharacterEntryLoginFlags : byte {
    None = 0,
    Locked = 1, // Lobby#64: "You cannot select this character with your current account."
    NameChangeRequired = 2, // Lobby#26: "A name change is required to log in with this character."
    MissingExVersionForLogin = 4, // Lobby#68: "To log in with this character you must first install <ExVersion>."
    MissingExVersionForCharacterEdit = 8, // Lobby#69: "To edit this character's race, sex, or appearance you must first install <ExVersion>."
    DCTraveling = 16,// Lobby#1175: "This character is currently visiting the <value> data center."
    Unk32 = 32, // unsure. sidebar should change to Lobby#1153 "TRAVELED TO" and might print LogMessage#5800 "Unable to execute command. Character is currently visiting the <string(lstr1)> data center."
}

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x58)]
public unsafe partial struct CharaSelectRetainerInfo {
    [FieldOffset(0)] public ulong RetainerId;
    [FieldOffset(0x8)] public ulong OwnerContentId;
    [FieldOffset(0x10)] public ushort Index; // guessed
    [FieldOffset(0x12)] public CharaSelectRetainerInfoLoginFlags LoginFlags;

    [FieldOffset(0x18), FixedSizeArray(isString: true)] internal FixedSizeArray32<byte> _name;
}

public enum CharaSelectRetainerInfoLoginFlags : ushort {
    None = 0,

    NameChangeRequired = 4 // Lobby#66: "Please change your retainer's name after retrieving your character's data./To log in with this character you must first change your retainer's name."
}

[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0x1E2)]
public unsafe partial struct CharaSelectCharacterInfo {
    [FieldOffset(0x8), FixedSizeArray(isString: true)] internal FixedSizeArray32<byte> _name;
    [FieldOffset(0x28)] public byte CurrentClassJobId;

    [FieldOffset(0x2A), FixedSizeArray] internal FixedSizeArray30<ushort> _classJobLevels;
    [FieldOffset(0x66)] public byte Race;
    [FieldOffset(0x67)] public byte Tribe;
    [FieldOffset(0x68)] public byte Sex;
    [FieldOffset(0x69)] public byte BirthMonth;
    [FieldOffset(0x6A)] public byte BirthDay;
    [FieldOffset(0x6B)] public byte GuardianDeity;
    [FieldOffset(0x6C)] public byte FirstClass;

    // [FieldOffset(0x6E)] public ushort ZoneId;
    [FieldOffset(0x70)] public ushort TerritoryType;
    [FieldOffset(0x72)] public ushort ContentFinderCondition;
    [FieldOffset(0x74)] public CustomizeData CustomizeData;

    [FieldOffset(0x90)] public WeaponModelId MainHandModel;
    [FieldOffset(0x98)] public WeaponModelId OffHandModel;
    [FieldOffset(0xA0)] public EquipmentModelId Head;
    [FieldOffset(0xA4)] public EquipmentModelId Body;
    [FieldOffset(0xA8)] public EquipmentModelId Hands;
    [FieldOffset(0xAC)] public EquipmentModelId Legs;
    [FieldOffset(0xB0)] public EquipmentModelId Feet;
    [FieldOffset(0xB4)] public EquipmentModelId Ears;
    [FieldOffset(0xB8)] public EquipmentModelId Neck;
    [FieldOffset(0xBC)] public EquipmentModelId Wrists;
    [FieldOffset(0xC0)] public EquipmentModelId RingRight;
    [FieldOffset(0xC4)] public EquipmentModelId RingLeft;
    [FieldOffset(0xC8)] public uint MainHandItemId;
    [FieldOffset(0xCC)] public uint OffHandItemId;
    [FieldOffset(0xD0)] public uint SoulstoneItemId;
    // [FieldOffset(0xD4)] public uint RemakeFlag;
    [FieldOffset(0xD8)] public CharaSelectCharacterConfigFlags ConfigFlags;
    [FieldOffset(0xDA)] public byte VoiceId;
    // [FieldOffset(0xDB), FixedSizeArray] internal FixedSizeArray32<byte> _worldName; // always empty?

    // [FieldOffset(0x100)] public ulong LoginStatus;
    // [FieldOffset(0x108)] public byte IsOutTerritory;
}

[Flags]
public enum CharaSelectCharacterConfigFlags : ushort {
    None = 0,
    HideHead = 0x01,
    HideWeapon = 0x02,
    HideLegacyMark = 0x04,
    // ? = 0x08,
    StoreNewItemsInArmouryChest = 0x10,
    StoreCraftedItemsInInventory = 0x20,
    CloseVisor = 0x40
    // ? = 0x80
}
