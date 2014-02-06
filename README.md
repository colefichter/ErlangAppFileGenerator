This program helps to copy a .app.src file from \src\myapp.app.src to \ebin\myapp.app.

During the process, it replaces the string `[MODULES]` with the list of erlang module names found in \src\.

Usage (works great in a batch file:

```
ErlangAppFileGenerator.exe myapp
```

Note: be sure to run it from the parent folder of \ebin\ and \src\.