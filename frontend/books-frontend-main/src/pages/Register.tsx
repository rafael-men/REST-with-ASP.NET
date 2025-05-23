import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Form, FormField, FormItem, FormLabel, FormControl, FormMessage } from "@/components/ui/form";
import { useToast } from "@/components/ui/use-toast";
import { useNavigate, Link } from "react-router-dom";
import { useState } from "react";

const registerSchema = z.object({
  userName: z.string().min(1, "Usuário é obrigatório"),
  password: z.string().min(1, "Senha é obrigatória"),
  fullName: z.string().min(1, "Nome completo é obrigatório"),
});

type RegisterData = z.infer<typeof registerSchema>;

export default function Register() {
  const { toast } = useToast();
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const form = useForm<RegisterData>({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      userName: "",
      password: "",
      fullName: "",
    },
  });

  const onSubmit = async (data: RegisterData) => {
    setIsSubmitting(true);
    try {
      const response = await fetch("https://rest-with-asp-net.onrender.com/v1/api/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (response.status === 409) {
        throw new Error("Nome de usuário já está em uso.");
      }

      if (!response.ok) {
        throw new Error("Erro ao registrar usuário.");
      }

      toast({
        title: "Usuário cadastrado com sucesso!",
        description: "Voltando para o Login...",
      });

      setTimeout(() => {
        navigate("/login");
      }, 2000);
    } catch (error: any) {
      toast({
        title: "Erro",
        description: error.message || "Ocorreu um erro.",
        variant: "destructive",
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div
      className="min-h-screen  bg-center flex items-center  bg-[url('https://c1.wallpaperflare.com/preview/127/366/443/library-book-bookshelf-read.jpg')] justify-center">
      <div className="bg-black bg-opacity-70 p-8 rounded-xl w-full max-w-md shadow-lg">
        <h1 className="text-2xl font-bold mb-6 text-white text-center">Cadastro</h1>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
            <FormField
              control={form.control}
              name="fullName"
              render={({ field }) => (
                <FormItem>
                  <FormLabel className="text-white">Nome completo</FormLabel>
                  <FormControl>
                    <Input placeholder="Digite seu nome completo" {...field} />
                  </FormControl>
                  <FormMessage className="text-red-400" />
                </FormItem>
              )}
            />

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
              {isSubmitting ? "Cadastrando..." : "Cadastrar"}
            </Button>
          </form>
        </Form>

        <p className="text-white text-sm text-center mt-4">
          Já tem conta?{" "}
          <Link to="/login" className="no-underline text-blue-400 hover:text-blue-300">
            Fazer login
          </Link>
        </p>
      </div>
    </div>
  );
}
