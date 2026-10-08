# Lenovo Keyboard Backlight Control

A small C# utility that uses Lenovo's own Vantage APIs to programmatically control keyboard backlight settings on supported Lenovo laptops.

## What It Does

This tool loads Lenovo's installed Vantage components and changes the keyboard backlight mode using the same interface used by Lenovo Vantage.

Discovered supported modes:

| Mode | Description |
|--------|-------------|
| Off | Keyboard backlight off |
| Auto | Backlight automatically turns off after inactivity |
| Level_1 | Low brightness |
| Level_2 | High brightness |

On the tested system, setting the mode to `Level_1` or `Level_2` keeps the keyboard backlight illuminated continuously.

## How It Works

The program:

1. Loads Lenovo's installed assemblies:
   - `IdeaNotebookAddin.dll`
   - `KeyboardContract.dll`

2. Creates a `KeyboardSettingsRequest`

3. Sets:

```text
KeyboardBacklightStatus = Level_1
```

4. Calls Lenovo's internal:

```text
SetBacklightStatus()
```

API.

This approach uses Lenovo's own software and firmware interfaces rather than simulating key presses or modifying registry values.

## Compatibility

Tested on:

```text
Lenovo IdeaPad Slim 5 14AGP11 (83S1)
```

using Lenovo Vantage.

### Likely Compatible

- Recent IdeaPad laptops
- IdeaPad Slim series
- Some Yoga models
- Other consumer Lenovo laptops using Lenovo Vantage and the `IdeaNotebookAddin` architecture

### May Not Work

- ThinkPads
- Older Lenovo laptops
- Systems using Commercial Vantage
- Systems without:
  - `IdeaNotebookAddin.dll`
  - `KeyboardContract.dll`

The program depends on Lenovo's internal Vantage APIs and is not intended as a universal Lenovo backlight controller.

## Disclaimer

This project relies on undocumented Lenovo components discovered through runtime inspection of Lenovo Vantage. Future Lenovo updates may change or remove these APIs.
