using System.Collections.Generic;


namespace LibReports
{
    class Filters
    {
        private List<string> batchName;
        private List<string> recipeName;
        private List<string> userName;
        private string startDate;
        private string endDate;

        public string formatString;


        public Filters() { }

        public Filters(List<string> batch, List<string> model, List<string> user, string sDate, string eDate) {
            this.batchName = new List<string>(batch);
            this.recipeName = new List<string>(model);
            this.userName = new List<string>(user);
            this.startDate = sDate;
            this.endDate = eDate;

            this.GenerateString();

            int i = 5;
        }

        private void GenerateString()
        {
            this.formatString = "";

            if(this.startDate == "" & this.endDate == "")
            {
            }
            else
            {
                this.formatString += "WHERE InspectionStartTime >= " + this.startDate + " AND InspectionStopTime <= " + this.endDate + " ";
            }

            if(this.batchName.Count == 0)
            {

            }
            else
            {
                if(this.formatString=="")
                {
                    this.formatString += "WHERE BatchNo IN ('";
                    for (int i = 0; i<this.batchName.Count; i++)
                    {
                        this.formatString += this.batchName[i];
                        
                        if (i+1 == this.batchName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                    //this.formatString += "WHERE BatchNo IN ('" + this.batchName + "') ";
                }
                else
                {
                    this.formatString += "AND BatchNo IN ('";
                    for (int i = 0; i < this.batchName.Count; i++)
                    {
                        this.formatString += this.batchName[i];

                        if (i + 1 == this.batchName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                }
            }
            if(this.recipeName.Count == 0)
            {

            }
            else
            {
                if (this.formatString == "")
                {
                    this.formatString += "WHERE ModelName IN ('";
                    for (int i = 0; i < this.recipeName.Count; i++)
                    {
                        this.formatString += this.recipeName[i];

                        if (i + 1 == this.recipeName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                }
                else
                {
                    this.formatString += "AND ModelName IN ('";
                    for (int i = 0; i < this.recipeName.Count; i++)
                    {
                        this.formatString += this.recipeName[i];

                        if (i + 1 == this.recipeName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                }
            }
            if(this.userName.Count == 0)
            {

            }
            else
            {
                if (this.formatString == "")
                {
                    this.formatString += "WHERE InspectedBy IN ('";
                    for (int i = 0; i < this.userName.Count; i++)
                    {
                        this.formatString += this.userName[i];

                        if (i + 1 == this.userName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                }
                else
                {
                    this.formatString += "AND InspectedBy IN ('";
                    for (int i = 0; i < this.userName.Count; i++)
                    {
                        this.formatString += this.userName[i];

                        if (i + 1 == this.userName.Count)
                        {
                            this.formatString += "')";
                        }
                        else
                        {
                            this.formatString += "','";
                        }
                    }
                }
            }
        }

        public List<string> GetBatch()
        {
            return this.batchName;
        }

        public List<string> GetRecipe()
        {
            return this.recipeName;
        }

        public List<string> GetUser()
        {
            return this.userName;
        }
    }
}
