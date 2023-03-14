import os


AppName = "ConsoleApp"

solution_directory = os.path.dirname(os.path.realpath(__file__))
programDirectory = os.path.join(
    solution_directory, AppName, "bin", "Debug", "net7.0")
os.chdir(programDirectory)


def TestDays(command):
    fullCommand = " ".join([AppName, command])
    print(fullCommand)
    os.system(fullCommand)
    print("\n")


TestDays("add --category Holiday --description Midsummer --date 1989-06-24")
TestDays("add --category Birthday --description \"Tuukka's Birthday\" --date 2000-05-23")
TestDays("add --description CurrentDay")

TestDays("list")
TestDays("list --category birthday")
TestDays("list --category Birthday")
TestDays("list --description tuukka")
TestDays("list --description Tuukka")
TestDays("list --date 2000-05-23")
TestDays("list --before-date 2000-05-23")
TestDays("list --after-date 2000-05-23 --exclude")
TestDays("list --after-date 2000-05-23")
TestDays("list --before-date 2000-05-23 --exclude")

TestDays("list --today")
TestDays("delete --all --dry-run")
TestDays("delete --description tuukka")
TestDays("delete --description mid")
TestDays("delete --today")

input('Press ENTER to exit')
