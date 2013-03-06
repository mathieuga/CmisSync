//   CmisSync, a collaboration and sharing tool.
//   Copyright (C) 2010  Hylke Bons <hylkebons@gmail.com>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using CmisSync.Lib;

namespace CmisSync.Lib.Cmis
{

    // Sets up a fetcher that can get remote folders
    public class SparkleFetcher : FetcherBase
    {
        RepoCmis CmisRepo;

        //public SparkleFetcher(string server, string required_fingerprint, string remote_path,
        //    string target_folder, bool fetch_prior_history, string canonical_name, string repository, string path,
        //    string user, string password, SparkleConfig config, ActivityListener activityListener)
        //    : base(server, required_fingerprint,
        //        remote_path, target_folder, fetch_prior_history, repository, path, user, password)
        public SparkleFetcher(RepoInfo repoInfo, ActivityListener activityListener)
            : base(repoInfo)
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher constructor");
            TargetFolder = repoInfo.TargetDirectory;
            RemoteUrl = repoInfo.Address;

            // String localPath = Path.Combine(SparkleFolder.ROOT_FOLDER, repoInfo.TargetDirectory);
            // String localPath = Path.Combine(SparkleConfig.DefaultConfig.FoldersPath, repoInfo.TargetDirectory);

            if (!Directory.Exists(Config.DefaultConfig.FoldersPath))
            {
                Logger.LogInfo("Fecther", String.Format("ERROR - Cmis Default Folder {0} do not exist", Config.DefaultConfig.FoldersPath));
                throw new DirectoryNotFoundException("Root folder don't exist !");
            }

            if (!SparkleFolder.HasWritePermissionOnDir(Config.DefaultConfig.FoldersPath))
            {
                Logger.LogInfo("Fetcher", String.Format("ERROR - Cmis Default Folder {0} is not writable", Config.DefaultConfig.FoldersPath));
                throw new UnauthorizedAccessException("Root folder is not writable!");
            }

            if (Directory.Exists(repoInfo.TargetDirectory))
            {
                Logger.LogInfo("Fetcher", String.Format("ERROR - Cmis Repository Folder {0} already exist", repoInfo.TargetDirectory));
                throw new UnauthorizedAccessException("Repository folder already exist!");
            }

            Directory.CreateDirectory(repoInfo.TargetDirectory);

            CmisRepo = new RepoCmis(repoInfo, activityListener);

            // CmisDirectory cmis = new CmisDirectory(canonical_name, path, remote_path, server, user, password, repository, activityListener);
            // cmis.Sync();
        }


        public override bool Fetch()
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher Fetch");
            //double percentage = 1.0;
            //Regex progress_regex = new Regex(@"([0-9]+)%", RegexOptions.Compiled);
            //DateTime last_change = DateTime.Now;
            //TimeSpan change_interval = new TimeSpan(0, 0, 0, 1);

            CmisRepo.DoFirstSync();
            return true; // TODO
        }


        public override bool IsFetchedRepoEmpty
        {
            get
            {
                Logger.LogInfo("Fetcher", "Cmis SparkleFetcher IsFetchedRepoEmpty");
                return false; // TODO
            }
        }


        public override void EnableFetchedRepoCrypto(string password)
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher EnableFetchedRepoCrypto");
            // TODO
        }


        public override bool IsFetchedRepoPasswordCorrect(string password)
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher IsFetchedRepoPasswordCorrect");
            return true; // TODO
        }


        public override void Stop()
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher Stop");
        }


        public override void Complete()
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher Complete");
            // base.Complete();
        }


        private void InstallConfiguration()
        {
            Logger.LogInfo("Fetcher", "Cmis SparkleFetcher InstallConfiguration");
        }

    }
}