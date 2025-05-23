import { useState, useEffect, ChangeEvent, FormEvent } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Navbar from "@/components/Navbar";
import { Button } from "@/components/ui/button";
import { useToast } from "@/components/ui/use-toast";

interface Book {
  id: number;
  title: string;
  author: string;
  price: number;
  launchDate: string;
  description: string;
  image: string;
}

export default function EditBook() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { toast } = useToast();

  const [book, setBook] = useState<Book | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const fetchBook = async () => {
      setLoading(true);
      try {
        const token = localStorage.getItem("authToken");
        if (!token) {
          navigate("/login");
          return;
        }

        const response = await fetch(`https://rest-with-asp-net.onrender.com/v1/api/books/${id}`, {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        });

        if (response.status === 401) {
          toast({
            title: "Sessão expirada",
            description: "Faça login novamente.",
            variant: "destructive",
          });
          navigate("/login");
          return;
        }

        if (!response.ok) {
          throw new Error("Livro não encontrado");
        }

        const data: Book = await response.json();
        setBook(data);
      } catch (error) {
        toast({
          title: "Erro",
          description: "Não foi possível carregar os dados do livro",
          variant: "destructive",
        });
        navigate("/books");
      } finally {
        setLoading(false);
      }
    };

    if (id) {
      fetchBook();
    }
  }, [id, navigate, toast]);

  // Atualiza campos do livro no estado
  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    if (!book) return;
    const { name, value } = e.target;

    setBook({
      ...book,
      [name]: name === "price" ? parseFloat(value) || 0 : value,
    });
  };

 
  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    if (!book) return;

    setSaving(true);

    try {
      const token = localStorage.getItem("authToken");
      if (!token) {
        navigate("/login");
        return;
      }

      const response = await fetch(`https://rest-with-asp-net.onrender.com/v1/api/books/update`, {
        method: "PUT",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(book),
      });

      if (response.status === 401) {
        toast({
          title: "Sessão expirada",
          description: "Faça login novamente.",
          variant: "destructive",
        });
        navigate("/login");
        return;
      }

      if (!response.ok) {
        throw new Error("Erro ao salvar o livro");
      }

      toast({
        title: "Sucesso",
        description: "Livro atualizado com sucesso!",
      });

      navigate(`/books/${id}`);
    } catch (error) {
      toast({
        title: "Erro",
        description: "Não foi possível atualizar o livro",
        variant: "destructive",
      });
    } finally {
      setSaving(false);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50">
        <Navbar />
        <div className="container mx-auto px-4 py-8">
          <div className="flex justify-center items-center h-64">
            <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary"></div>
          </div>
        </div>
      </div>
    );
  }

  if (!book) return null;

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="container mx-auto px-4 py-8 page-transition">

  
        <h1 className="text-2xl font-bold mb-6">Editar Livro</h1>

        <form onSubmit={handleSubmit} className="bg-white rounded-xl shadow-md p-8 max-w-lg mx-auto">
          <label className="block mb-4">
            <span className="text-gray-700 font-semibold">Título</span>
            <input
              type="text"
              name="title"
              value={book.title}
              onChange={handleChange}
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>

          <label className="block mb-4">
            <span className="text-gray-700 font-semibold">Autor</span>
            <input
              type="text"
              name="author"
              value={book.author}
              onChange={handleChange}
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>

          <label className="block mb-4">
            <span className="text-gray-700 font-semibold">Preço (R$)</span>
            <input
              type="number"
              name="price"
              value={book.price}
              onChange={handleChange}
              step="0.01"
              min="0"
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>

          <label className="block mb-4">
            <span className="text-gray-700 font-semibold">Data de Lançamento</span>
            <input
              type="date"
              name="launchDate"
              value={book.launchDate.slice(0, 10)} 
              onChange={handleChange}
              required
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>

          <label className="block mb-4">
            <span className="text-gray-700 font-semibold">Descrição</span>
            <textarea
              name="description"
              value={book.description}
              onChange={handleChange}
              rows={4}
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>

          <label className="block mb-6">
            <span className="text-gray-700 font-semibold">URL da Imagem</span>
            <input
              type="url"
              name="image"
              value={book.image}
              onChange={handleChange}
              className="mt-1 block w-full rounded-md border border-gray-300 px-3 py-2"
            />
          </label>
          <Button type="submit"  disabled={saving}>
            {saving ? "Salvando..." : "Salvar Alterações"}
          </Button>
          <Button type="submit" className="ml-10 " onClick={() => navigate(`/books/${id}`)}>
            Voltar
          </Button>
        </form>
      </main>
    </div>
  );
}
