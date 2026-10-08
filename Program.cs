using System;
using System.Collections;
using System.Reflection;

var addin = Assembly.LoadFrom(
    @"C:\ProgramData\Lenovo\Vantage\Addins\IdeaNotebookAddin\1.0.13.114\IdeaNotebookAddin.dll");

var contract = Assembly.LoadFrom(
    @"C:\ProgramData\Lenovo\Vantage\Addins\IdeaNotebookAddin\1.0.13.114\KeyboardContract.dll");

//
// Get Lenovo keyboard agent
//
var agentType =
    addin.GetType("IdeaNotebookAddin.IdeaNotebookAgent");

var agent =
    agentType!
        .GetMethod("GetInstance")!
        .Invoke(null, null);

//
// Build Setting
//
var settingType =
    contract.GetType(
        "Lenovo.Modern.Contracts.Keyboard.Setting");

var setting =
    Activator.CreateInstance(settingType!);

settingType!
    .GetProperty("key")!
    .SetValue(setting,
        "KeyboardBacklightStatus");

settingType!
    .GetProperty("value")!
    .SetValue(setting,
        "Level_1");

//
// Build SettingList
//
var settingListType =
    contract.GetType(
        "Lenovo.Modern.Contracts.Keyboard.SettingList");

var settingList =
    Activator.CreateInstance(settingListType!);

var itemsProperty =
    settingListType!
        .GetProperty("Items");

var itemsType =
    itemsProperty!.PropertyType;

var items =
    Activator.CreateInstance(itemsType);

itemsType
    .GetMethod("Add")!
    .Invoke(items,
        new[] { setting });

itemsProperty.SetValue(
    settingList,
    items);

//
// Build Request
//
var requestType =
    contract.GetType(
        "Lenovo.Modern.Contracts.Keyboard.KeyboardSettingsRequest");

var request =
    Activator.CreateInstance(requestType!);

requestType!
    .GetProperty("List")!
    .SetValue(request,
        settingList);

//
// Call Lenovo API
//
Console.WriteLine("Setting Level_1...");

var result =
    agentType!
        .GetMethod("SetBacklightStatus")!
        .Invoke(agent,
            new[] { request });

Console.WriteLine();
Console.WriteLine("RESULT:");
Console.WriteLine(result);

//
// Read back settings
//
Console.WriteLine();
Console.WriteLine("READ BACK:");

var response =
    agentType!
        .GetMethod("GetKeyboardSettings")!
        .Invoke(agent, null);

var responseList =
    response!
        .GetType()
        .GetProperty("List")!
        .GetValue(response);

var responseItems =
    (IEnumerable)
    responseList!
        .GetType()
        .GetProperty("Items")!
        .GetValue(responseList)!;

foreach (var item in responseItems)
{
    var t = item.GetType();

    Console.WriteLine(
        $"Key={t.GetProperty("key")?.GetValue(item)}, " +
        $"Value={t.GetProperty("value")?.GetValue(item)}");
}
