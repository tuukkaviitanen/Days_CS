# DAYS C#

Calendar event management console application
Queries and writes calendar events from and to a CSV file
Project uses .NET 7

## HOW TO USE

1. Create a directory called `.days` in your HOME DIRECTORY (ON WINDOWS C:/Users/{username}) (ON LINUX: /home/{username}) (usually)
2. Create `Events.csv` file inside `.days`
3. BUILD Days project either in Visual Studio 2022 or in console using `dotnet build` (.NET 7 SDK needed)
4. Program is now runnable...
   a. with `dotnet run [params]`
      OR
   b. in `Days/bin/Debug/net7.0` ON WINDOWS CMD: `days [params]`, ON WINDOWS POWERSHELL: `./days [params]`, ON LINUX: `./days.exe [params]`

## PARAMS

Same documentation is also found in program (try: --help, list --help).
README UPDATED ON 2023-03-16. Most up-to-date params documentation can always be found in-app.
```
  list       List events to console

    --category       Filter all Events in selected category. (Case INSENSITIVE)

    --categories     Filter all Events in selected categories. Seperated by comma ',' (Case INSENSITIVE)

    --description    Filter all Events matching description (even partial match) (Case INSENSITIVE)

    --date           Filter all Events in selected date

    --today          Filter all Events happening today

    --after-date     Filter all Events after the selected date

    --before-date    Filter all Events prior the selected date

    --no-category    Filter all Events with no category

    --exclude        Reverse all used filters

  add        Add event

    --category

    --description    Required.

    --date

  delete     Delete event(s). Displays all deleted/would-be-deleted events. At least one option from group 'Queries' is required.

    --category       (Group: Queries) Filter all Events in selected category. (Case INSENSITIVE)

    --description    (Group: Queries) Filter all Events matching description (even partial match) (Case INSENSITIVE)

    --date           (Group: Queries) Filter all Events in selected date

    --today          (Group: Queries) Filter all Events happening today

    --after-date     (Group: Queries) Filter all Events after the selected date

    --before-date    (Group: Queries) Filter all Events prior the selected date

    --no-category    (Group: Queries) Filter all Events with no category

    --exclude        (Group: Queries) Reverse all used filters

    --all            (Group: Queries) Delete ALL Events

    --dry-run        Only displays Events that would be deleted with used filters
```
## TESTING WITH SCRIPT

After building the app in debug configuration, you should be able to run `test_script.py`.
It runs most possible iterations of the app through console.

HOW-TO:

In `Days` directory run with:

On Windows `python test_script.py`
On Linux `python3 test_script.py`

## NuGet Packages

- CsvHelper (30.0.1) by Josh Close
- CommandLineParser (2.9.1) by gsscoder, nemec, ericnewton76 and moh-hassan

## Tools used

- Visual Studio 2022
- Visual Studio Code (for python testing script and README)
