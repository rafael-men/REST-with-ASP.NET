import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Form, FormField, FormItem, FormLabel, FormControl, FormMessage } from "@/components/ui/form";
import { useToast } from "@/components/ui/use-toast";
import { useNavigate, Link } from "react-router-dom";
import { useState } from "react";

const loginSchema = z.object({
  userName: z.string().min(1, "Usuário é obrigatório"),
  password: z.string().min(1, "Senha é obrigatória"),
});

type LoginData = z.infer<typeof loginSchema>;

export default function Login() {
  const { toast } = useToast();
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const form = useForm<LoginData>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      userName: "",
      password: "",
    },
  });

 const onSubmit = async (data: LoginData) => {
  setIsSubmitting(true);
  try {
    const response = await fetch("https://rest-with-asp-net.onrender.com/v1/api/auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error("Credenciais inválidas");
    }

    const result = await response.json();
    const token = result.token;

 
    localStorage.setItem("authToken", token);
    

    navigate("/main");
  } catch (error: any) {
    toast({
      title: "Erro no login",
      description: error.message || "Verifique suas credenciais.",
      variant: "destructive",
    });
  } finally {
    setIsSubmitting(false);
  }
};

  return (
    <div
      className="min-h-screen bg-cover bg-center flex items-center justify-center"
      style={{ backgroundImage: "url('https://c1.wallpaperflare.com/preview/127/366/443/library-book-bookshelf-read.jpg')" }}
    >
      <div className="bg-black bg-opacity-70 p-8 rounded-xl w-full max-w-md shadow-lg">
        <h1 className="text-2xl font-bold mb-6 text-white text-center">Login</h1>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="userName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-white">Usuário</FormLabel>
                  <FormControl>
                    <Input placeholder="Digite seu usuário" {...field} />
                  </FormControl>
                  <FormMessage className="text-red-400" />
                </FormItem>
              )}
            />

            <FormField
              control={form.control}
              name="password"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-white">Senha</FormLabel>
                  <FormControl>
                    <Input type="password" placeholder="Digite sua senha" {...field} />
                  </FormControl>
                  <FormMessage className="text-red-400" />
                </FormItem>
              )}
            />

            <Button type="submit" className="w-full" disabled={isSubmitting}>
              {isSubmitting ? "Entrando..." : "Entrar"}
            </Button>
          </form>
        </Form>

        <p className="text-white text-sm text-center mt-4">
          Ainda não tem conta?{" "}
          <Link to="/register" className="no-underline text-blue-400 hover:text-blue-300">
            Cadastre-se
          </Link>
        </p>
      </div>
    </div>
  );
}
