using System;
using System.Diagnostics;

namespace GitFirstPushForGitHub
{
    class Program
    {
        public string workingDirectory;
        static void Main(string[] args)
        {
            /* githubに上げたい作業用フォルダの入力 */
            Console.WriteLine("Please write your git directory");
            var gitDirectoryPath = Console.ReadLine();

            /* githubのリポジトリの入力 */
            Console.WriteLine("Please write your github repository");
            var githubRepositoryPath = Console.ReadLine();

            /* コミット時のコメント */
            var comment = "\"first commit\"";

            /* githubに上げたいフォルダを引数として、クラスのインスタンス化を行う。 */
            UpdateForGit updateForGit = new UpdateForGit(gitDirectoryPath);
            /* フォルダ内のgit環境を整える。 */
            updateForGit.gitCommand($@"init");
            updateForGit.gitCommand($@"remote add origin {githubRepositoryPath}");
            updateForGit.gitCommand($@"add *");
            /* ローカルコミット */
            updateForGit.gitCommand($@"commit -m {comment}");
            /* githubのリモートにプッシュする。 */
            updateForGit.gitCommand($@"push origin master");
        }

    }
    class UpdateForGit
    {
        private string workingDirectory;
        public UpdateForGit(string workingDirectory)
        {
            this.workingDirectory = workingDirectory;
        }
        private int executeCommand(string command, string arguments = "")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(command)
                {
                    Arguments = arguments,
                    UseShellExecute = false,
                    WorkingDirectory = this.workingDirectory,
                }
            };
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
        public void gitCommand(string arguments)
        {
            if (executeCommand("git", arguments) != 0)
            {
                throw new Exception("git command error");
            }
        }
    }
}
