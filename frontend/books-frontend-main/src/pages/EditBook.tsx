
import { useState, useEffect } from "react";
import { useNavigate, useParams, Link } from "react-router-dom";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import Navbar from "@/components/Navbar";
import { Book, BookFormData } from "@/types/BookTypes";
import { 
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useToast } from "@/components/ui/use-toast";
import { ArrowLeft } from "lucide-react";

// Mock data for initial development (same as in other files)
const mockBooks: Book[] = [
  {
    id: "1",
    title: "Dom Casmurro",
    author: "Machado de Assis",
    description: "Clássico da literatura brasileira que narra a história de Bentinho e seu ciúme por Capitu.",
    genre: "Clássico",
    pages: 256,
    publicationYear: 1899,
    publisher: "Garnier",
    isbn: "9788574801711",
    status: "available",
    coverUrl: "https://m.media-amazon.com/images/I/81ZgkYVAjML._AC_UF1000,1000_QL80_.jpg",
    rating: 5,
    createdAt: "2023-01-15T10:30:00Z",
    updatedAt: "2023-01-15T10:30:00Z",
  },
  {
    id: "2",
    title: "O Pequeno Príncipe",
    author: "Antoine de Saint-Exupéry",
    description: "Uma história sobre um príncipe que vive em um pequeno planeta e viaja pelo universo.",
    genre: "Fábula",
    pages: 96,
    publicationYear: 1943,
    publisher: "Agir",
    isbn: "9788574801728",
    status: "borrowed",
    coverUrl: "https://m.media-amazon.com/images/I/71QM1Je34AL._AC_UF1000,1000_QL80_.jpg",
    rating: 4,
    createdAt: "2023-01-18T14:20:00Z",
    updatedAt: "2023-01-18T14:20:00Z",
  },
  {
    id: "3",
    title: "1984",
    author: "George Orwell",
    description: "Distopia que retrata um mundo totalitário e vigilante.",
    genre: "Ficção Distópica",
    pages: 328,
    publicationYear: 1949,
    publisher: "Companhia das Letras",
    isbn: "9788574801735",
    status: "reserved",
    coverUrl: "https://m.media-amazon.com/images/I/819js3EQwbL._AC_UF1000,1000_QL80_.jpg",
    rating: 5,
    createdAt: "2023-01-20T09:15:00Z",
    updatedAt: "2023-01-20T09:15:00Z",
  },
  {
    id: "4",
    title: "Memórias Póstumas de Brás Cubas",
    author: "Machado de Assis",
    description: "Romance narrado por um defunto autor, que conta sua história e reflexões após a morte.",
    genre: "Clássico",
    pages: 192,
    publicationYear: 1881,
    publisher: "Principis",
    isbn: "9788574801742",
    status: "available",
    coverUrl: "https://m.media-amazon.com/images/I/71cGFqTSDvL._AC_UF1000,1000_QL80_.jpg",
    rating: 5,
    createdAt: "2023-01-25T16:40:00Z",
    updatedAt: "2023-01-25T16:40:00Z",
  },
];

const bookSchema = z.object({
  title: z.string().min(1, "Título é obrigatório"),
  author: z.string().min(1, "Autor é obrigatório"),
  description: z.string().min(1, "Descrição é obrigatória"),
  genre: z.string().min(1, "Gênero é obrigatório"),
  pages: z.coerce.number()
    .int("O número de páginas deve ser um número inteiro")
    .positive("O número de páginas deve ser positivo"),
  publicationYear: z.coerce.number()
    .int("O ano de publicação deve ser um número inteiro")
    .min(1000, "O ano de publicação deve ser válido")
    .max(new Date().getFullYear(), `O ano não pode ser maior que ${new Date().getFullYear()}`),
  publisher: z.string().min(1, "Editora é obrigatória"),
  isbn: z.string().min(1, "ISBN é obrigatório"),
  status: z.enum(["available", "borrowed", "reserved"]),
  coverUrl: z.string().url("URL inválida").optional().or(z.literal("")),
  rating: z.coerce.number().min(1).max(5).optional(),
});

export default function EditBook() {
  const { id } = useParams<{ id: string }>();
  const [book, setBook] = useState<Book | null>(null);
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();
  const { toast } = useToast();

  const form = useForm<BookFormData>({
    resolver: zodResolver(bookSchema),
    defaultValues: {
      title: "",
      author: "",
      description: "",
      genre: "",
      pages: 0,
      publicationYear: 0,
      publisher: "",
      isbn: "",
      status: "available",
      coverUrl: "",
      rating: undefined,
    },
  });

  useEffect(() => {
    // Simulating API fetch
    const fetchBook = async () => {
      try {
        // In a real app, you would fetch from your API
        // const response = await fetch(`your-api-endpoint/books/${id}`);
        // const data = await response.json();
        
        // Using mock data for now
        setTimeout(() => {
          const foundBook = mockBooks.find(b => b.id === id);
          if (foundBook) {
            setBook(foundBook);
            form.reset({
              title: foundBook.title,
              author: foundBook.author,
              description: foundBook.description,
              genre: foundBook.genre,
              pages: foundBook.pages,
              publicationYear: foundBook.publicationYear,
              publisher: foundBook.publisher,
              isbn: foundBook.isbn,
              status: foundBook.status,
              coverUrl: foundBook.coverUrl || "",
              rating: foundBook.rating,
            });
          } else {
            toast({
              title: "Erro",
              description: "Livro não encontrado",
              variant: "destructive",
            });
            navigate("/books");
          }
          setLoading(false);
        }, 800);
      } catch (error) {
        toast({
          title: "Erro",
          description: "Não foi possível carregar os detalhes do livro",
          variant: "destructive",
        });
        setLoading(false);
      }
    };

    fetchBook();
  }, [id, toast, navigate, form]);

  const onSubmit = async (data: BookFormData) => {
    setIsSubmitting(true);
    try {
      // In a real app, you would call your API
      // await fetch(`your-api-endpoint/books/${id}`, {
      //   method: 'PUT',
      //   headers: {
      //     'Content-Type': 'application/json',
      //   },
      //   body: JSON.stringify(data),
      // });
      
      // Simulating API call
      console.log("Form data to be updated:", data);
      
      // Simulate delay and success
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      toast({
        title: "Sucesso",
        description: "Livro atualizado com sucesso",
      });
      
      navigate(`/books/${id}`);
    } catch (error) {
      toast({
        title: "Erro",
        description: "Não foi possível atualizar o livro",
        variant: "destructive",
      });
    } finally {
      setIsSubmitting(false);
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

  return (
    <div className="min-h-screen bg-gray-50">
      <Navbar />
      <main className="container mx-auto px-4 py-8 max-w-3xl page-transition">
        <Link to={`/books/${id}`} className="inline-flex items-center text-primary mb-6 hover:underline">
          <ArrowLeft className="h-4 w-4 mr-1" />
          Voltar para detalhes do livro
        </Link>

        <div className="bg-white rounded-xl shadow-md p-6">
          <h1 className="text-2xl font-bold text-gray-900 mb-6">Editar Livro</h1>

          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6">
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <FormField
                  control={form.control}
                  name="title"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Título</FormLabel>
                      <FormControl>
                        <Input placeholder="Título do livro" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="author"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Autor</FormLabel>
                      <FormControl>
                        <Input placeholder="Nome do autor" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="genre"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Gênero</FormLabel>
                      <FormControl>
                        <Input placeholder="Gênero do livro" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="publisher"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Editora</FormLabel>
                      <FormControl>
                        <Input placeholder="Nome da editora" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="pages"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Número de Páginas</FormLabel>
                      <FormControl>
                        <Input type="number" placeholder="0" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="publicationYear"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Ano de Publicação</FormLabel>
                      <FormControl>
                        <Input type="number" placeholder="2023" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="isbn"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>ISBN</FormLabel>
                      <FormControl>
                        <Input placeholder="ISBN do livro" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="status"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Status</FormLabel>
                      <Select onValueChange={field.onChange} value={field.value}>
                        <FormControl>
                          <SelectTrigger>
                            <SelectValue placeholder="Selecione o status" />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          <SelectItem value="available">Disponível</SelectItem>
                          <SelectItem value="borrowed">Emprestado</SelectItem>
                          <SelectItem value="reserved">Reservado</SelectItem>
                        </SelectContent>
                      </Select>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="coverUrl"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>URL da Capa (opcional)</FormLabel>
                      <FormControl>
                        <Input placeholder="https://exemplo.com/imagem.jpg" {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />

                <FormField
                  control={form.control}
                  name="rating"
                  render={({ field }) => (
                    <FormItem>
                      <FormLabel>Avaliação (1-5) (opcional)</FormLabel>
                      <FormControl>
                        <Input 
                          type="number" 
                          min="1" 
                          max="5" 
                          placeholder="Avaliação de 1 a 5"
                          {...field} 
                          value={field.value || ""}
                          onChange={(e) => {
                            const value = e.target.value ? parseInt(e.target.value, 10) : undefined;
                            field.onChange(value);
                          }}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>

              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Descrição</FormLabel>
                    <FormControl>
                      <Textarea 
                        placeholder="Descreva o livro" 
                        className="resize-none min-h-[120px]"
                        {...field} 
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <div className="flex justify-end space-x-4">
                <Button 
                  type="button" 
                  variant="outline" 
                  onClick={() => navigate(`/books/${id}`)}
                >
                  Cancelar
                </Button>
                <Button 
                  type="submit" 
                  className="bg-primary hover:bg-primary/90"
                  disabled={isSubmitting}
                >
                  {isSubmitting ? "Salvando..." : "Salvar Alterações"}
                </Button>
              </div>
            </form>
          </Form>
        </div>
      </main>
    </div>
  );
}
