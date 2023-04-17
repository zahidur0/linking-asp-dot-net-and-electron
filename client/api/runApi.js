module.exports = () => {
  const { spawn } = require("child_process");
  const child = spawn(
    `${__dirname}/published-api/linking-asp-dot-net-and-electron.exe`
  );

  child.stdout.on("data", (data) => {
    console.log(`stdout: ${data}`);
  });

  child.stderr.on("data", (data) => {
    console.error(`stderr: ${data}`);
  });
};
