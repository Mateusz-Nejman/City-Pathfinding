namespace Nejman.CPF
{
    public class ProcessCheckPoint
    {
        public string pointName;
        public string parentCheckpoint;

        public ProcessCheckPoint(string name, string parent)
        {
            pointName = name;
            parentCheckpoint = parent;
        }
    }
}
