namespace main.VO.Converter.Contract
{
    public interface IParser<O,D>
    {
        D parse(O origin);

        List<D> parse(List<O> origin);
    }
}
