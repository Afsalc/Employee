PGDMP                         y            Employee_db    12.3    12.3 	               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    434395    Employee_db    DATABASE     �   CREATE DATABASE "Employee_db" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_India.1252' LC_CTYPE = 'English_India.1252';
    DROP DATABASE "Employee_db";
                postgres    false            �            1259    434398    master_employee    TABLE       CREATE TABLE public.master_employee (
    pkemp_id integer NOT NULL,
    employee_name character varying(200) NOT NULL,
    employee_desigination character varying(200) NOT NULL,
    employee_dob date NOT NULL,
    employee_phonenumber character varying(20) NOT NULL
);
 #   DROP TABLE public.master_employee;
       public         heap    postgres    false            �            1259    434396    master_employee_pkemp_id_seq    SEQUENCE     �   ALTER TABLE public.master_employee ALTER COLUMN pkemp_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.master_employee_pkemp_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    203                       0    434398    master_employee 
   TABLE DATA           }   COPY public.master_employee (pkemp_id, employee_name, employee_desigination, employee_dob, employee_phonenumber) FROM stdin;
    public          postgres    false    203   �	                  0    0    master_employee_pkemp_id_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('public.master_employee_pkemp_id_seq', 6, true);
          public          postgres    false    202            �
           2606    434402 $   master_employee master_employee_pkey 
   CONSTRAINT     h   ALTER TABLE ONLY public.master_employee
    ADD CONSTRAINT master_employee_pkey PRIMARY KEY (pkemp_id);
 N   ALTER TABLE ONLY public.master_employee DROP CONSTRAINT master_employee_pkey;
       public            postgres    false    203                �   x�M�1�0@��>r��M�JH��X�D�V���:��?}���|��iz̥78[o�n�st$�%�ʁho�U8t�����ꂨ
F�i.v��	O�2�&fሺ�����YB�&eB��uB�G	���G���,�     