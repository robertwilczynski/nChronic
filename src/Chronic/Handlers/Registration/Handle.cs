namespace Chronic.Handlers
{
    public static class Handle
    {
        public static HandlerBuilder Optional<THandler>()
        {
            return new HandlerBuilder().Optional<THandler>();
        }

        public static HandlerBuilder Required<THandler>()
        {
            return new HandlerBuilder().Required<THandler>();
        }
    }
}