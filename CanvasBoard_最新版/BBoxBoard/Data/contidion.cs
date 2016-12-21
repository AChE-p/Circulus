namespace BBoxBoard.Data
{
    public  class condition
    {
        public double precision_time;
        public double sum_time;
        public enum presecion_condition_enum {fast_mode, general_mode, hquality_mode};
        public presecion_condition_enum presion_condition;
        public  condition()
        {
            precision_time = 1e-6;
            presion_condition = new presecion_condition_enum();
        }
    }
}
